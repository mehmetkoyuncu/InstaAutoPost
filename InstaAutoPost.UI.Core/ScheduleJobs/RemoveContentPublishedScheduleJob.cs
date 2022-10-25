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
    public static class RemoveContentPublishedScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment,string cron)
        {
            RecurringJob.AddOrUpdate(() => RemoveContentPublished(environment),cron);
        }
        public static void RemoveContentPublished(string environment)
        {
            MailService mailService = new MailService();
            var result = new SourceContentService().RemoveAllPublishedContent(environment);

            if (result > 0)
            {
                mailService.SendMailAutoJob(mailService.ReplaceConfigure(MailContentConstants.AutoJobContent, JobNamesConstants.RemoveContentPublished, removedContent: result.ToString()), mailService.ReplaceConfigure(MailSubjectConstants.AutoJobSubject, JobNamesConstants.RemoveContentPublished));
              
                Log.Logger.Information($"{JobNamesConstants.RemoveContentPublished} - Job Başarıyla çalıştırıldı - {DateTime.Now}");
            }
            else
                Log.Logger.Error($"{JobNamesConstants.RemoveContentPublished} - Job çalıştırılırken hata oluştu - {DateTime.Now}");
        }
    }
}
