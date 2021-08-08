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
            List<CategoryDTO> categories = _categoryService.GetSourceWithCategoriesById(sourceId);
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml",categories);
        }
    }
}
