
using InstaAutoPost.UI.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var client = new RestClient("https://localhost:44338/source/getallsources");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Name", "\"Deneme\"");
            IRestResponse response = client.Execute(request);
            var weatherForecast = JsonSerializer.Deserialize<SourceModel>(response.Content);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
