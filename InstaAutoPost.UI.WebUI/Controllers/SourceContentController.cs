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
    public class SourceContentController : Controller
    {
        ISourceContentService _sourceContentService;
        private readonly IHostEnvironment _environment;
        public SourceContentController(ISourceContentService service, IHostEnvironment environment)
        {
            _sourceContentService = service;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetAllContent()
        {
            List<SourceContentDTO> contents = _sourceContentService.GetSourceContentList();
            return PartialView("~/Views/Shared/Partials/_SourceContentListPartial.cshtml", contents);
        }


        public PartialViewResult GetAddSourceContentPartial()
        {
            List<SelectboxSourceDTO> sources = _sourceContentService.GetSourcesIdAndName();
            return PartialView("~/Views/Shared/Partials/_SourceContentAddPartial.cshtml", sources);
        }

        public JsonResult GetCategoryIdAndName(int sourceId)
        {
            var a = _sourceContentService.GetCategoriesIdAndName(sourceId);
            return Json(_sourceContentService.GetCategoriesIdAndName(sourceId));
        }
        public JsonResult AddSourceContent(SourceContentDTO sourceContent)
        {
            int result = _sourceContentService.AddSourceContent(sourceContent, _environment.ContentRootPath);
            return Json(result);
        }
        [HttpPut]
        public JsonResult EditSourceContent(SourceContentDTO sourceContent)
        {
            int result = _sourceContentService.EditSourceContent(sourceContent, _environment.ContentRootPath);
            return Json(result);
        }
        [HttpDelete]
        public IActionResult RemoveSourceContent(int id)
        {
            int result = _sourceContentService.RemoveSourceContent(id);
            return Json(result);
        }
        public IActionResult GetSourceContent(int id)
        {
            SourceContentDTO contents = _sourceContentService.GetSurceContentDTO(id);
            return PartialView("~/Views/Shared/Partials/_SourceContentDetailPartial.cshtml", contents);
        }
        public IActionResult GetSourcesIdAndNameList()
        {
            List<SelectboxSourceDTO> sources = _sourceContentService.GetSourcesIdAndName();
            return PartialView("~/Views/Shared/Partials/_SourceContentOptionsPartial.cshtml", sources);
        }
        public IActionResult Filter(int categoryId, int orderId, string searchText)
        {
            List<SourceContentDTO> contents = _sourceContentService.Filter(categoryId, orderId, searchText);
            return PartialView("~/Views/Shared/Partials/_SourceContentListPartial.cshtml", contents);
        }
    }
}
