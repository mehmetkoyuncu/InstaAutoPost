using Hangfire;
using InstaAutoPost.UI.Core.Common.Constants;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static class RemoveSentMailsScheduleJob
    {
        [Obsolete]
        public static void RunJob(string cron)
        {
            RecurringJob.AddOrUpdate(() => RemoveSentMails(), cron);
        }
        public static void RemoveSentMails()
        {
            MailService mailService = new MailService();

           var result= mailService.RemoveAllMail();
            if (result > 0)
            {
                mailService.SendMailAutoJob(mailService.ReplaceConfigure(MailContentConstants.AutoJobContent, JobNamesConstants.RemoveSentMails, removedContent: result.ToString()), mailService.ReplaceConfigure(MailSubjectConstants.AutoJobSubject, JobNamesConstants.RemoveSentMails));
                Log.Logger.Information($"{JobNamesConstants.RemoveSentMails} - Job Başarıyla çalıştırıldı - {DateTime.Now}");
            }
            else
                Log.Logger.Error($"{JobNamesConstants.RemoveSentMails} - Job çalıştırılırken hata oluştu - {DateTime.Now}");

        }
    }
}
