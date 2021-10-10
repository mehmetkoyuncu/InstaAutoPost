using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class RssRunnerController : Controller
    {
        IRssRunnerService _rssGeneratorService;
        IHostEnvironment _environment;

        public IActionResult Index()
        {
            return View();
        }
        public RssRunnerController(IRssRunnerService service, IHostEnvironment environment)
        {
            _rssGeneratorService = service;
            _environment = environment;
        }
        [HttpPost]
        public IActionResult RunRssGenerator(string url, string name)
        {
            RssResultDTO result = _rssGeneratorService.RunRssGenerator(url, name, _environment);
            if (result != null)
                return Ok(Json(result));
            else
                return Ok(Json("İçerikler kaydedilemedi"));
        }
        public PartialViewResult GetRSSGeneratorPartial()
        {
            return PartialView("~/Views/Shared/Partials/_RSSGeneratorAddPartial.cshtml");
        }
    }
}
