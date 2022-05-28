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
        ICategoryTypeService _categoryTypeService;

        public IActionResult Index()
        {
            return View();
        }
        public RssRunnerController(IRssRunnerService service, IHostEnvironment environment, ICategoryTypeService categoryTypeService)
        {
            _rssGeneratorService = service;
            _environment = environment;
            _categoryTypeService = categoryTypeService;
        }
        [HttpPost]
        public IActionResult RunRssGenerator(string url, int categoryTypeId)
        {
            RssResultDTO result = _rssGeneratorService.RunRssGenerator(url, categoryTypeId, _environment.ContentRootPath);
            return Ok(Json(result));
        }
        public PartialViewResult GetRSSGeneratorPartial()
        {
            var types = _categoryTypeService.GetAllCategoryType();
            return PartialView("~/Views/Shared/Partials/_RSSGeneratorAddPartial.cshtml",types);
        }
    }
}
