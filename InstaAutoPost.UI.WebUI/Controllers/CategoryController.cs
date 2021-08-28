﻿using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        public IActionResult Index()
        {
            return View();
        }

        #region Kategori Sil
        [HttpDelete]
        public IActionResult RemoveCategory(int id)
        {
            return Ok(_categoryService.RemoveCategory(id));
        }
        #endregion
        #region Kategori Ekle Partial'ı Getir
        [HttpGet]
        public PartialViewResult GetAddCategoryPartial()
        {
            var sourceList = _categoryService.GetSourcesIdandName();
            return PartialView("~/Views/Shared/Partials/_CategoryAddPartial.cshtml", sourceList);
        }
        #endregion
        #region Kategori Ekle
        [HttpPost]
        public JsonResult AddCategory(string name, int sourceId)
        {
            int result = _categoryService.AddCategory(name, sourceId);
            return Json(result);
        }
        #endregion
        #region Kategorileri Getir
        public PartialViewResult GetAllCategories()
        {
            List<CategoryDTO> categories = _categoryService.GetAllCategories();
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml", categories);
        }
        #endregion
        #region Filtre
        public IActionResult Filter(int sourceId, int orderId, string searchText)
        {
            List<CategoryDTO> categories = _categoryService.Filter(sourceId, orderId, searchText);
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml", categories);
        }
        #endregion
        #region Kategori Düzenle
        [HttpPut]
        public IActionResult EditCategory(int id, string name, int sourceId)
        {
            int result = _categoryService.EditCategory(id, name, sourceId);
            return Json(result);
        }
        #endregion
        #region Id'ye Göre Kategori Listesini Getir
        public PartialViewResult GetCategoryBySourceId(int id, string searchText)
        {
            List<CategoryDTO> categories = _categoryService.GetAllCategoryBySourceId(id, searchText);
            return PartialView("~/Views/Shared/Partials/_CategoryListPartial.cshtml", categories);
        }
        #endregion
        #region Source id ve isimlerini getir
        public IActionResult GetSourcesIdAndNameList()
        {
            List<SelectboxSourceDTO> sources = _categoryService.GetSourcesIdandName();
            return PartialView("~/Views/Shared/Partials/_CategoryOptionsPartial.cshtml", sources);
        }
        #endregion

    }
}
