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

    }
}
