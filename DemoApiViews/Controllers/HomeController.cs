using DemoApiViews.DTO;
using DemoApiViews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System;
using RestSharp;
using System.Text.Json;



namespace DemoApiViews.Controllers
{
    public class HomeController : Controller
    {
        //Get users
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetData(UsersDto usersDto)
        {
            // Serialize the user input to JSON
            string userInputJson = JsonSerializer.Serialize(usersDto);

            // Create a RestRequest to send the serialized JSON to the API
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7237/api/user/register", Method.Post);
            request.AddJsonBody(userInputJson);
            RestResponse response = client.Execute(request);
            ViewData["data"] = response.Content;
            ViewData["responsetype"] = response.StatusCode;
            return View();
        }
    }
}