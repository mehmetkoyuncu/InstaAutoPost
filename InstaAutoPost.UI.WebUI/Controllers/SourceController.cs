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
    public class SourceController : Controller
    {
        private readonly ISourceService _sourceService;
        private readonly IHostEnvironment _environment;
        public SourceController(ISourceService sourceService,IHostEnvironment environmet)
        {
            _sourceService = sourceService;
            _environment = environmet;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Kaynakları Getir
        public PartialViewResult GetAllSources()
        {
            List<SourceDTO> sources = _sourceService.GetAllSources();
            return PartialView("~/Views/Shared/Partials/_SourceListPartial.cshtml", sources);
        }
        #endregion
        #region Kaynak Ekle Partial'ı Getir
        [HttpGet]
        public PartialViewResult GetAddSourcePartial()
        {
            return PartialView("~/Views/Shared/Partials/_SourceAddPartial.cshtml");
        }
        #endregion
        #region Kaynak Ekle
        [HttpPost]
        public JsonResult AddSource(string name,string image)
        {
            int result = _sourceService.AddSource(name,image,_environment.ContentRootPath);
            return Json(result);
        }
        #endregion
        #region Kaynak Düzenle
        [HttpPut]
        public IActionResult EditSource(int id, string name, string image)
        {
            int result = _sourceService.EditSource(id, name, image,_environment.ContentRootPath);
            return Json(result);
        }
        #endregion
        #region Kaynak Sil
        [HttpDelete]
        public IActionResult RemoveSource(int id)
        {
            return Ok(_sourceService.RemoveSource(id));
        }
        #endregion
        #region Filtreyi Getir
        public IActionResult GetSourceFilterView()
        {
            return PartialView("~/Views/Shared/Partials/_SourceOptionsPartial.cshtml");
        }
        #endregion
        #region Filtre
        public IActionResult Filter(int orderId, string searchText)
        {
            List<SourceDTO> sources = _sourceService.Filter(orderId,searchText);
            return PartialView("~/Views/Shared/Partials/_SourceListPartial.cshtml", sources);
        }

        #endregion

    }
}
