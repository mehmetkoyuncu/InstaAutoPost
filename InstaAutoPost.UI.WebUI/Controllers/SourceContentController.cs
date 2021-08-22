using InstaAutoPost.UI.Core.Abstract;
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
        public PartialViewResult GetSourceContent(int categoryId)
        {
            return PartialView("~/Views/Shared/Partials/_SourceContentListPartial.cshtml", _sourceContentService.GetSourceContent(categoryId));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
