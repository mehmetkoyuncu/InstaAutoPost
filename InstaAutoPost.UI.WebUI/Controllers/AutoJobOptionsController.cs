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
    public class AutoJobOptionsController : Controller
    {
        private readonly IAutoJobService _autoJobService;
        IHostEnvironment _environment;
        public AutoJobOptionsController(IAutoJobService autoJobService,IHostEnvironment environment)
        {
            _autoJobService = autoJobService;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAutoJobOptionsPartial()
        {
            var autoJobs=_autoJobService.GetAutoJobs();
            return PartialView("~/Views/Shared/Partials/_AutoJobListPartial.cshtml",autoJobs);
        }
        public IActionResult EditAutoJob(AutoJobDTO autoJobDTO)
        {
            var result = _autoJobService.UpdateAutoJob(autoJobDTO,_environment.ContentRootPath);
            return Json(result);
        }
    }
}
