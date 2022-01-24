using Hangfire;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using InstaAutoPost.UI.Core.AutoMapper;


namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static class CreateFolderScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment)
        {
            RecurringJob.AddOrUpdate(() => CreateFolderScheduleJob.CreateFolder(environment), "*/30 * * * *");
        }
        public static void CreateFolder(string environment)
        {
            SourceContentService contentService = new SourceContentService();
            List<SourceContent> contentList = contentService.GetSourceContenListNotDeleted();
            SourceContent content = contentList.Where(x=>x.IsCreatedFolder==false&&x.SendOutForPost==false).OrderBy(x => x.ContentInsertAt).ToList().FirstOrDefault();
            MailService mailService = new MailService();
            SourceContentDTO sourceContentDTO = Mapping.Mapper.Map<SourceContentDTO>(content);
            if (content!=null)
            {
                bool result = contentService.CreateFolder(content.Id, environment);
                content.IsCreatedFolder = true;
                int updateResult = contentService.UpdateSourceContent(content);
                mailService.SendMailAutoForSourceContent(sourceContentDTO, environment);
            }
            else
            {
                mailService.SendMailAutoForSourceContent(sourceContentDTO, environment);
            }
            
        }
    }
}
