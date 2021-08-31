using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class SourceContentController : Controller
    {
        ISourceContentService _sourceContentService;
        public SourceContentController(ISourceContentService service)
        {
            _sourceContentService = service;
        }
        public IActionResult Index()
        {
            return View();
        }







        public PartialViewResult GetAllContent()
        {
            List<SourceContentDTO> contents = _sourceContentService.GetSourceContentList();
            return PartialView("~/Views/Shared/Partials/_SourceContentListPartial.cshtml",contents);
        }
        public PartialViewResult GetAddContentPartial()
        {
            List<SelectboxCategoryDTO> categories = _sourceContentService.GetCategoriesIdAndName();
            List<SelectboxSourceDTO> sources = _sourceContentService.GetSourcesIdAndName();
            Tuple<List<SelectboxCategoryDTO>,List<SelectboxSourceDTO> >result=Tuple.Create<List<SelectboxCategoryDTO>,List<SelectboxSourceDTO>>(categories, sources);
            return PartialView("~/Views/Shared/Partials/_SourceContentAddPartial.cshtml",result);
        }






        [HttpDelete]
        public IActionResult RemoveSourceContent(int id)
        {
            string result = _sourceContentService.RemoveSourceContent(id);
            return Json(result);
        }
    }
}
