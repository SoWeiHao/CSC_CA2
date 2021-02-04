using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stripe;
using Stripe.Checkout;
using WebApplication2.Models;

namespace server.Controllers
{
    public class PaymentsController : Controller
    {

        public readonly IOptions<StripeOptions> options;
        private readonly IStripeClient client;


        public PaymentsController(IOptions<StripeOptions> options)
        {
            this.options = options;
            this.client = new StripeClient(this.options.Value.SecretKey);
        }

        [HttpGet("setup")]
        public SetupResponse Setup()
        {
            return new SetupResponse
            {
                ProPrice = this.options.Value.ProPrice,
                BasicPrice = this.options.Value.BasicPrice,
                PublishableKey = this.options.Value.PublishableKey,
            };
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
        {
            Debug.WriteLine("path " + Request.Host);
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"https://{Request.Host}/home/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"https://{Request.Host}/cancel.html",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                CustomerEmail = User.Identity.Name,
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = req.PriceId,
                        Quantity = 1,
                    },
                },
            };
            var service = new SessionService(this.client);
            try
            {
                var session = await service.CreateAsync(options);
                return Ok(new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message,
                    }
                });
            }
        }


        [HttpGet("checkout-session")]
        public async Task<IActionResult> CheckoutSession(string sessionId)
        {
            var service = new SessionService(this.client);
            var session = await service.GetAsync(sessionId);
            return Ok(session);
        }

        [HttpPost("customer-portal")]
        public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalRequest req)
        {
            // For demonstration purposes, we're using the Checkout session to retrieve the customer ID. 
            // Typically this is stored alongside the authenticated user in your database.
            var checkoutSessionId = req.SessionId;
            var checkoutService = new SessionService(this.client);
            var checkoutSession = await checkoutService.GetAsync(checkoutSessionId);

            // This is the URL to which your customer will return after
            // they are done managing billing in the Customer Portal.
            var returnUrl = this.options.Value.Domain;

            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = checkoutSession.CustomerId,
                ReturnUrl = returnUrl,
            };
            var service = new Stripe.BillingPortal.SessionService(this.client);
            var session = await service.CreateAsync(options);

            return Ok(session);
        }

    }
}
