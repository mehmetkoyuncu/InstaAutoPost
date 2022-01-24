using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetMailMenu()
        {
            return PartialView("~/Views/Shared/Partials/_MailMenuPartial.cshtml");
        }
        public IActionResult GetSentMails()
        {
            var mailList = _mailService.GetSentEmailList();
            return PartialView("~/Views/Shared/Partials/_SentMailInboxPartial.cshtml",mailList);
        }
        public IActionResult GetAccountSetting()
        {
            var mailAuthenticate = _mailService.GetByMailAuthenticateByMailAddress();
            return PartialView("~/Views/Shared/Partials/_MailAccountPartial.cshtml",mailAuthenticate);
        }
        public IActionResult CreateAuthenticate(MailAuthenticate authenticate)
        {
            var result = _mailService.CreateMailAuthenticate(authenticate);
            return Json(result);
        }
        public IActionResult GetMailOptions()
        {
            var options = _mailService.GetByMailOptionDTO();
            return PartialView("~/Views/Shared/Partials/_MailOptions.cshtml", options);
        }
        public IActionResult CreateMailOptions(MailOptionsDTO optionsDTO)
        {
            var result = _mailService.CreateMailOptions(optionsDTO);
            return Json(result);
        }
        [HttpPost]
        public IActionResult SendMailDefault(MailOptionsDTO optionsDTO)
        {
            var result= _mailService.SendMailDefault(optionsDTO);
            return Json(result);
        }
        public IActionResult GetSendMailView()
        {
            var options = _mailService.GetByMailOptionDTO();
            return PartialView("~/Views/Shared/Partials/_MailOptions.cshtml");
        }


    }
}
