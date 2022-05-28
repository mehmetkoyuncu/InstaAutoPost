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
    public class SocialMediaAccountsCategoryTypeController : Controller
    {
        private readonly ISocialMediaAccountsCategoryTypeService _socialMediaCategoryService;
        private readonly IHostEnvironment _environment;
        private readonly ISocialMediaService _socialMediaService;
        private readonly ICategoryTypeService _categoryTypeService;
        public SocialMediaAccountsCategoryTypeController(ISocialMediaAccountsCategoryTypeService socialMediaCategoryService, IHostEnvironment environmet,ISocialMediaService socialMediaService,ICategoryTypeService categoryTypeService)
        {
            _socialMediaCategoryService = socialMediaCategoryService;
            _environment = environmet;
            _socialMediaService = socialMediaService;
            _categoryTypeService = categoryTypeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Kaynakları Getir
        public PartialViewResult GetAllSocialMediaAccountCategories()
        {
            List<SocialMediaAccountsCategoryTypeDTO> socialMedias = _socialMediaCategoryService.GetAll();
            return PartialView("~/Views/Shared/Partials/_SocialMediaAccountsCategoryListPartial.cshtml", socialMedias);
        }
        #endregion
        #region Kaynak Ekle Partial'ı Getir
        [HttpGet]
        public PartialViewResult GetAddSocialMediaAccountCategoryPartial()
        {
            var socialMedias = _socialMediaService.GetAll();
            var categoryTypes = _categoryTypeService.GetAllCategoryType();
            var model = new CategoryTypeSocialMediaListModel()
            {
                CategoryTypes = categoryTypes,
                SocialMedias = socialMedias
            };
            return PartialView("~/Views/Shared/Partials/_SocialMediaAccountsCategoryAddPartial.cshtml", model);
        }
        #endregion
        #region Kaynak Ekle
        [HttpPost]
        public JsonResult AddSocialMediaAccountCategory(SocialMediaAccountsCategoryTypeDTO socialMedia)
        {
            int result = _socialMediaCategoryService.Add(socialMedia);
            return Json(result);
        }
        #endregion
        [HttpPut]
        public IActionResult EditSocialMediaAccountCategoryType(SocialMediaAccountsCategoryTypeDTO socialMedia)
        {
            int result = _socialMediaCategoryService.Edit(socialMedia);
            return Json(result);
        }
        [HttpDelete]
        public IActionResult RemoveSocialMediaAccountCategoryType(int id)
        {
            return Ok(_socialMediaCategoryService.Remove(id));
        }
        public JsonResult GetSocialMediaAccountCategoryById(int id)
        {
            var media = _socialMediaCategoryService.GetDTO(id);
            return Json(media);
        }

    }
}
