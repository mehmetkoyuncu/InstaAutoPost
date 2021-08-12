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
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
            
        public PartialViewResult GetCategoriesWithSource(int sourceId)
        {
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml", _categoryService.GetSourceWithCategoriesById(sourceId));
        }
        [HttpDelete]
        public IActionResult RemoveCategory(int id)
        {
            return Ok(_categoryService.RemoveCategory(id));
        }
        [HttpGet]
        public PartialViewResult GetAddCategoryPartial()
        {
            return PartialView("~/Views/Shared/Partials/_CategoryAddPartial.cshtml");
        }
        [HttpPost]
        public PartialViewResult AddCategory(string name,string url,int sourceId)
        {
            _categoryService.AddCategory(name, url,sourceId);
            return PartialView("~/Views/Shared/Partials/_CategoryAddPartial.cshtml");
        }
        public PartialViewResult GetAllCategories()
        {
            List<CategoryDTO> categories = _categoryService.GetAllCategories();
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml",categories);
        }
        public PartialViewResult RunRssGenerator(string name, string url)
        {
            _categoryService.RunRssGenerator(url, name);
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml", _categoryService.GetAllCategories());
        }
        

    }
}
