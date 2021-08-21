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
        public PartialViewResult _SourcePartial()
        {
            var model = _sourceService.GetAll();
            model = model.OrderByDescending(x => x.UpdatedAt).ToList();
            return PartialView("~/Views/Shared/Partials/_SourcePartial.cshtml",model);
        }
        [HttpPost]
       
        [HttpPost]
        public IActionResult EditSource(string image,string name,int id)
        {
            string result = _sourceService.Update(image,name,id);
            return Ok(Json(result));
        }
        [HttpDelete]
        public IActionResult RemoveSource(int id)
        {
            string result = _sourceService.DeleteById(id);
            return Ok(Json(result));
        }
        [HttpGet]
        public PartialViewResult GetUpsertPartial()
        {
            return PartialView("~/Views/Shared/Partials/_UpsertPartial.cshtml");
        }

    }
}
