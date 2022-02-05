using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class AutoJobOptions : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
