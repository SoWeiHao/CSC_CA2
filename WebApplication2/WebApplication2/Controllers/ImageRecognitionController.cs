using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Clarifai.Channels;
using Clarifai.Api;
using Grpc.Core;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace WebApplication2.Controllers
{
    static class Keys
    {
        public const string ENVIRONMENT_URL = "api.veryfi.com";
        public const string USERNAME = "";
        public const string API_KEY = "";
        public const string CLIENT_ID = "";
    }

    public class ImageRecognitionController : ApiController
    {

        public Boolean ImageRecognition(IFormFile file)
        {
            if (file != null)
            {
                var response = PostModelOutputsWithFileBytes(file);
                return response;
            }
            else
            {
                return false;
            }
        }

        public Boolean PostModelOutputsWithFileBytes(IFormFile file)
        {
            Debug.WriteLine(file.FileName);
            ByteString bytes;
            //string path = @"D:\" + fileName + "." + fileExtension;

            var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string s = Convert.ToBase64String(fileBytes);
            bytes = ByteString.FromBase64(s);

            var client = new V2.V2Client(ClarifaiChannel.Grpc());

            var metadata = new Metadata
            {
                { "Authorization", "Key "}
            };
            var response = client.PostModelOutputs(
                new PostModelOutputsRequest()
                {
                    ModelId = "",
                    Inputs =
                    {
                        new List<Input>()
                        {
                            new Input()
                            {
                                Data = new Clarifai.Api.Data()
                                {
                                    Image = new Image()
                                    {
                                        Base64 = bytes
                                    }
                                }
                            }
                        }
                    }
                },
                metadata
            );
            if (response.Status.Code != Clarifai.Api.Status.StatusCode.Success)
                throw new Exception("Request failed, response: " + response);
            var isHuman = false;
            Console.WriteLine("Predicted concepts:");
            string output = "";
            foreach (var concept in response.Outputs[0].Data.Concepts)
            {
                if (concept.Name == "portrait" || concept.Name == "woman" || concept.Name == "adult" || concept.Name == "man" || concept.Name == "child" || concept.Name == "face" || concept.Name == "baby" || concept.Name == "human" || concept.Name == "son" || concept.Name == "girl" || concept.Name == "man")
                {
                    isHuman = true;
                }
                output += concept.Name + "\n, ";
                Console.WriteLine(isHuman);
                Console.WriteLine($"{concept.Name,15} {concept.Value:0.00}");
            }
            return isHuman;
        }


    }
}
