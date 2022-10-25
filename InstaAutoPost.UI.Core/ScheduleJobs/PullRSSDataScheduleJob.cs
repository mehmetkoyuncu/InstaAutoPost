using Hangfire;
using InstaAutoPost.UI.Core.Common.Constants;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static  class PullRSSDataScheduleJob
    { 
        [Obsolete]
        public static void RunJob(string environment,string cron)
        {
            RecurringJob.AddOrUpdate(() => PullRSSContent(environment), cron);
        }
        public static void PullRSSContent(string environment)
        {
            MailService mailService = new MailService();
            var rssList = new RSSListUtility().CreateRssList();
            if (rssList.Count > 0)
            {
                foreach (var item in rssList)
                {
                    if (item.CategoryURL != null)
                    {
                        RssResultDTO rssItem = new RssRunnerService().RunRssGenerator(item.CategoryURL, item.CategoryTypeId, environment);
                        SourceService sourceService = new SourceService();
                     
                        Source source = sourceService.GetSourceByCategoryLink(item.CategoryURL);
                        Category category = new CategoryService().GetById(rssItem.CategoryId);

                        mailService.SendMailAutoJob(mailService.ReplaceConfigure(MailContentConstants.PullRSSContent, resultDTO: rssItem, source: source, category: category), mailService.ReplaceConfigure(MailSubjectConstants.PullRSSSubject, resultDTO: rssItem, category: category));
                    }
                    
                }
                new SourceContentService().RemoveSameContents();
                Log.Logger.Information($"{JobNamesConstants.PullRSSContent} - Job Başarıyla çalıştırıldı - {DateTime.Now}");
                
            }
        }
    }
}
