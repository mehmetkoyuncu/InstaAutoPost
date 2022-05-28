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
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly IHostEnvironment _environment;
        public SocialMediaController(ISocialMediaService socialMediaService, IHostEnvironment environmet)
        {
            _socialMediaService = socialMediaService;
            _environment = environmet;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Kaynakları Getir
        public PartialViewResult GetAllSocialMedias()
        {
            List<SocialMediaDTO> socialMedias = _socialMediaService.GetAll();
            return PartialView("~/Views/Shared/Partials/_SocialMediaListPartial.cshtml", socialMedias);
        }
        #endregion
        #region Kaynak Ekle Partial'ı Getir
        [HttpGet]
        public PartialViewResult GetAddSocialMediaPartial()
        {
            return PartialView("~/Views/Shared/Partials/_SocialMediaAddPartial.cshtml");
        }
        #endregion
        #region Kaynak Ekle
        [HttpPost]
        public JsonResult AddSocialMedia(SocialMediaDTO socialMedia)
        {
            int result = _socialMediaService.Add(socialMedia);
            return Json(result);
        }
        #endregion
        [HttpPut]
        public IActionResult EditSocialMedia(SocialMediaDTO socialMedia)
        {
            int result = _socialMediaService.Edit(socialMedia);
            return Json(result);
        }
        [HttpDelete]
        public IActionResult RemoveSocialMedia(int id)
        {
            return Ok(_socialMediaService.Remove(id));
        }
        public JsonResult GetSocialMediaById(int id)
        {
            var media=_socialMediaService.GetDTO(id);
            return Json(media);
        }
    }
}
