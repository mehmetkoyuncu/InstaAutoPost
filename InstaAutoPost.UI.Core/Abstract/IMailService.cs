using InstaAutoPost.UI.Core.Common.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface IMailService
    {
         void SendMessage(MailDTO mailDTO, string rootPath = null, string fileName = null);
         string ConfigureSMTP(string fromMail);
         void AddMail(MailDTO mail);
         MailAuthenticate GetByMailAuthenticateByMailAddress();
        public int CreateMailAuthenticate(MailAuthenticate authenticateInfo);
        int CreateMailOptions(MailOptionsDTO options);
        MailOptionsDTO GetByMailOptionDTO();
        int SendMailDefault(MailOptionsDTO mailOptionsDTO);
        List<SentMailDTO> GetSentEmailList();

    }
}
