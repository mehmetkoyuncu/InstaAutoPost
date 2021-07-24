
using InstaAutoPost.UI.WebUI.Models;
using InstaAutoPost.UI.WebUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            ViewBag.breadCrump = "Anasayfa";
            return View();
        }
        public PartialViewResult _SourcePartial()
        {
            Request requestGetAllSources = new Request();
            string requestResult = requestGetAllSources.RequestGet("https://localhost:44338/source/getallsources");
            List<SourceModel> model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SourceModel>>(requestResult);
            model = model.OrderByDescending(x => x.UpdatedAt).ToList();
            return PartialView("~/Views/Shared/Partials/_SourcePartial.cshtml",model);
        }
    }
}
