using DemoApiViews.DTO;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace DemoApiViews.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7237/api/user", Method.Get);
            RestResponse response = client.Get(request);
            if (response.IsSuccessful)
            {
                //List<UsersDto>? users = JsonConvert.DeserializeObject<List<UsersDto>>(response.Content);
                List<UsersDto>? users = JsonSerializer.Deserialize<List<UsersDto>>(response.Content);
                ViewData["users"] = users;
                return View(users);
            }
            else
            {
                ViewData["users"] = null; // Handle the case where response is not successful
                return View();
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(string id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7237/api/user/"+id, Method.Delete);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditUser(string id) 
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7237/api/user/"+id, Method.Get);
            RestResponse response = client.Get(request);
            if (response.IsSuccessful)
            {
                //List<UsersDto>? users = JsonConvert.DeserializeObject<List<UsersDto>>(response.Content);
                UsersDto? users = JsonSerializer.Deserialize<UsersDto>(response.Content);
                ViewData["users"] = users;
                return View(users);
            }
            else
            {
                ViewData["users"] = null; // Handle the case where response is not successful
                return View();
            }
        }

        [HttpPost]
        public IActionResult UpdateUser(string id, UsersDto updatedUser)
        {
            //RestClient client = new RestClient();
            //RestRequest request = new RestRequest("https://localhost:7237/api/user/" + id, Method.Put);
            //RestResponse response = client.Execute(request);
            updatedUser.password = "";
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7237/api/user/" + id, Method.Put);
            string body = JsonSerializer.Serialize(updatedUser);
            request.AddJsonBody(body); // Serialize the updated user object to JSON and add it to the request body
            RestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}