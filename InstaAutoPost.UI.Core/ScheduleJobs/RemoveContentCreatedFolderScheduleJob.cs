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
    public static class RemoveContentCreatedFolderScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment, string cron)
        {
            RecurringJob.AddOrUpdate(() => RemoveContentCreatedFolder(environment), cron);
        }
        public static void RemoveContentCreatedFolder(string environment)
        {
            MailService mailService = new MailService();
            var result = new SourceContentService().RemoveAllCreatedFolderContent(environment);

            if (result > 0)
            {
                mailService.SendMailAutoJob(mailService.ReplaceConfigure(MailContentConstants.AutoJobContent, JobNamesConstants.RemoveContentCreatedFolder,removedContent:result.ToString()), mailService.ReplaceConfigure(MailSubjectConstants.AutoJobSubject, JobNamesConstants.RemoveContentCreatedFolder));
                OrderPostUtility.Order();
                Log.Logger.Information($"{JobNamesConstants.RemoveContentCreatedFolder} - Job Başarıyla çalıştırıldı - {DateTime.Now}");
            }
            else
                Log.Logger.Error($"{JobNamesConstants.RemoveContentCreatedFolder} - Job çalıştırılırken hata oluştu - {DateTime.Now}");
        }
    }
}
