using Hangfire;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static class CreateFolderScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment)
        {
            RecurringJob.AddOrUpdate(() => CreateFolderScheduleJob.CreateFolder(environment), Cron.HourInterval(1));
        }
        public static void CreateFolder(string environment)
        {
            SourceContentService contentService = new SourceContentService();
            List<SourceContent> contentList = contentService.GetSourceContenListNotDeleted();
            SourceContent content = contentList.Where(x=>x.IsCreatedFolder==false).OrderBy(x => x.ContentInsertAt).ToList().FirstOrDefault();
            bool result = contentService.CreateFolder(content.Id, environment);
            content.IsCreatedFolder = true;
            int updateResult = contentService.UpdateSourceContent(content);
        }
    }
}
