
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public IActionResult GetCategories(int id)
        {
            ViewBag.breadCrump = "Kategoriler";
            return View("~/Views/Category/Categories.cshtml",id);
        }
        public PartialViewResult _CategoryPartial(int sourceId)
        {
            List<CategoryDTO> categories = _service.GetSourceWithCategoriesById(sourceId);
            return PartialView("~/Views/Shared/Partials/_CategoryPartial.cshtml",categories);
        }

    }
}
