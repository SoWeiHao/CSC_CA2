using WebApplication2.Models;
using WebApplication2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly UserDetailsService _UserDetailsService;

        public UserDetailsController(UserDetailsService UserDetailsService)
        {
            _UserDetailsService = UserDetailsService;
        }

        [HttpGet]
        public ActionResult<List<UserDetails>> Get() =>
            _UserDetailsService.Get();

        [HttpGet("{id}")]
        public ActionResult<UserDetails> Get(string id)
        {
            var UserDetails = _UserDetailsService.Get(id);

            if (UserDetails == null)
            {
                return NotFound();
            }

            return UserDetails;
        }

        [HttpGet("{id}")]
        public string GetPlan(string id)
        {
            var UserDetails = _UserDetailsService.Get(id);

            if (UserDetails == null)
            {
                return "Not Found";
            }

            return UserDetails.Plan;
        }

        [HttpPost]
        public ActionResult<UserDetails> Create(UserDetails UserDetails)
        {
            _UserDetailsService.Create(UserDetails);

            return CreatedAtRoute("GetUserDetails", new { id = UserDetails.Id.ToString() }, UserDetails);
        }


    }
}