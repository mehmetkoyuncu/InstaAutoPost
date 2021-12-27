using InstaAutoPost.SendPostBot.Core.Selenium;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testim.Models;

namespace Testim.Controllers
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
            InstagramSelenium instagram = new InstagramSelenium();
            instagram.GoToMainPage();
            Console.WriteLine("Instagram anasayfası açıldı.");
            Thread.Sleep(2000);
            var userName = "haberdarmaymun";
            var password = "4421751mk";
            instagram.Login(userName, password);
            return null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
