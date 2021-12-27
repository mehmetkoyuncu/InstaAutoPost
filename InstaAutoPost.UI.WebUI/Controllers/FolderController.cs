using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class FolderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetFolderPartial()
        {
            return PartialView("~/Views/Shared/Partials/_FolderAddPartial.cshtml");
        }
    }
}
