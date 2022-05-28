using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class CategoryTypeController : Controller
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryTypeController(ICategoryTypeService categoryTypeService)
        {
            _categoryTypeService = categoryTypeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetAllCategoryTypes()
        {
            List<CategoryTypeDTO> categoryTypes = _categoryTypeService.GetAllCategoryType();
            return PartialView("~/Views/Shared/Partials/_CategoryTypeListPartial.cshtml", categoryTypes);
        }
        public PartialViewResult GetAddCategoryTypePartial()
        {
            return PartialView("~/Views/Shared/Partials/_CategoryTypeAddPartial.cshtml");
        }
        [HttpPost]
        public JsonResult AddCategoryType(string name)
        {
            int result = _categoryTypeService.Add(name);
            return Json(result);
        }
        [HttpPut]
        public IActionResult EditCategoryType(int id,string name)
        {
            int result = _categoryTypeService.EditSource(id,name);
            return Json(result);
        }
        [HttpDelete]
        public IActionResult RemoveCategoryType(int id)
        {
            return Ok(_categoryTypeService.RemoveCategoryType(id));
        }
        public IActionResult GetCategoryTypeById(int id)
        {
            CategoryTypeDTO categoryTypeDTO = _categoryTypeService.GetCategoryTypeDTO(id);
            return Ok(Json(categoryTypeDTO));
        }
    }
}
