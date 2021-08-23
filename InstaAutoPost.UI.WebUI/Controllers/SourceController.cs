using InstaAutoPost.UI.Core.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class SourceController : Controller
    {
        private readonly ISourceService _sourceService;
        public SourceController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public IActionResult Index()
        {
            ViewBag.BreadCrumpHeader="Kaynak";
            ViewBag.BreadCrumpText="Örnek Metin";
            ViewBag.BreadCrumpImage="https://i1.sndcdn.com/avatars-000288473023-nw5wu6-t500x500.jpg";

            return View();
        }

        [HttpGet]
        public PartialViewResult GetSources()
        {
            return PartialView("~/Views/Shared/Partials/_SourceListPartial.cshtml", _sourceService.GetAll());
        }
        [HttpGet]
        public PartialViewResult GetAddSourcesPartial()
        {
            return PartialView("~/Views/Shared/Partials/_SourceAddPartial.cshtml");
        }
        [HttpPost]
        public IActionResult AddSource(string name,string image,string url)
        {
            return Ok(_sourceService.Add(name, image,url));
        }
        [HttpGet]
        public IActionResult GetSourceById(int id)
        {
            return Ok(_sourceService.GetById(id));
        }
        [HttpPut]
        public IActionResult EditSource(int id,string name, string image)
        {
            return Ok(_sourceService.Update(image,name,id));
        }
        public IActionResult RemoveSource(int id)
        {
            return Ok(_sourceService.DeleteById(id));
        }
        public IActionResult DetailSource(int id)
        {
            return PartialView("~/Views/Shared/Partials/_SourceDetailPartial.cshtml", _sourceService.GetSourceWithCategoryCount(id));
        }
        public IActionResult GetSourceSelectContainer()
        {
            return PartialView("~/Views/Shared/Partials/_SelectContainerPartial.cshtml", _sourceService.GetSourcesForSelectBox());
        }
    }
}
