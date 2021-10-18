using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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
        public PartialViewResult GetAllContent(int next=0,int quantity=10)
        {
            var contents = _sourceContentService.GetSourceContentList(next,quantity);
            int totalCount = _sourceContentService.GetSourceContentCount();
            var result=Tuple.Create<List<SourceContentDTO>, int>(contents,totalCount);
            return PartialView("~/Views/Shared/Partials/_SourceContentListPartial.cshtml", result);
        }


        public PartialViewResult GetAddSourceContentPartial()
        {
            List<SelectboxSourceDTO> sources = _sourceContentService.GetSourcesIdAndName();
            return PartialView("~/Views/Shared/Partials/_SourceContentAddPartial.cshtml", sources);
        }

        public JsonResult GetCategoryIdAndName(int sourceId)
        {
            return Json(_sourceContentService.GetCategoriesIdAndName(sourceId));
        }
        public JsonResult AddSourceContent(SourceContentAddOrUpdateDTO sourceContent)
        {
            int result = _sourceContentService.AddSourceContent(sourceContent, _environment.ContentRootPath);
            if (!ModelState.IsValid)
                return Json(-1);
            return Json(result);
        }
        [HttpPut]
        public JsonResult EditSourceContent(int id,SourceContentAddOrUpdateDTO sourceContent)
        {
            int result = _sourceContentService.EditSourceContent(id,sourceContent, _environment.ContentRootPath);
            if (!ModelState.IsValid)
                return Json(-1);
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
        public IActionResult Filter(int categoryId, int orderId, string searchText,int next= 0, int quantity = 10)
        {
            List<SourceContentDTO> contents = _sourceContentService.Filter(categoryId, orderId, searchText);
            List<SourceContentDTO> contentFilter = _sourceContentService.GetSourceContentFilter(contents,next,quantity);
            var result = Tuple.Create<List<SourceContentDTO>, int>(contentFilter, contents.Count());
            return PartialView("~/Views/Shared/Partials/_SourceContentListPartial.cshtml", result);
        }
        #region Id'ye göre içeriği getir
        public IActionResult GetSourceContentById(int id)
        {
            SourceContentAddOrUpdateDTO sourceContent = _sourceContentService.GetSourceContentDTOById(id);
            return Ok(Json(sourceContent));
        }
        #endregion
    }
}
