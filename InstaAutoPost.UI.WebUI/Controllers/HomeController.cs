using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
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
        private readonly ISourceService _sourceService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISourceService sourceService)
        {
            _logger = logger;
            _sourceService = sourceService;
        }
        
        public IActionResult Index()
        {
            ViewBag.breadCrump = "Anasayfa";
            return View();
        }
        [HttpDelete]
        public IActionResult RemoveSource(int id)
        {
            int result = _sourceService.RemoveSource(id);
            return Ok(Json(result));
        }
        [HttpGet]
        public PartialViewResult GetUpsertPartial()
        {
            return PartialView("~/Views/Shared/Partials/_UpsertPartial.cshtml");
        }

    }
}
