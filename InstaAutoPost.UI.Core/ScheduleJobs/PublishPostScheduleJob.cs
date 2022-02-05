using Hangfire;
using InstaAutoPost.SendPostBot.UI;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static class PublishPostScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment)
        {
            RecurringJob.AddOrUpdate(() => CreateFolderScheduleJob.CreateFolder(environment), "*/20 * * * *");
        }
        public static void PublishPost(string environment)
        {
            SourceContentService contentService = new SourceContentService();
            List<SourceContent> contentList = contentService.GetSourceContenListNotDeleted();
            SourceContent content = contentList.Where(x => x.IsCreatedFolder == false && x.SendOutForPost == false).OrderBy(x => x.ContentInsertAt).ToList().FirstOrDefault();
            MailService mailService = new MailService();
            var instagramBot = new SeleniumMain().Publish(content, environment);
            SourceContentDTO sourceContentDTO = Mapping.Mapper.Map<SourceContentDTO>(content);
            if (content != null)
            {
                bool result = contentService.CreateFolder(content.Id, environment);
                content.IsCreatedFolder = true;
                int updateResult = contentService.UpdateSourceContent(content);
            }
            mailService.SendMailAutoForSourceContent(sourceContentDTO, environment);

        }
    }
}
