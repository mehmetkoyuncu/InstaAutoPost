
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategories(int id)
        {
            ViewBag.breadCrump = "Kategoriler";
            return View("~/Views/Category/Categories.cshtml",id);
        }
        public PartialViewResult _CategoryPartial(int sourceId)
        {
           
            //List<CategoryModel> model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CategoryModel>>(requestResult);
            //model = model.OrderByDescending(x => x.UpdatedAt).ToList();
            return PartialView("~/Views/Shared/Partials/_CategoryPartial.cshtml"/*, model*/);
        }

    }
}
