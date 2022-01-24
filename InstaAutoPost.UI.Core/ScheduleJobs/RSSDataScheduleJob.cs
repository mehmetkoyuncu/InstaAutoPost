using Hangfire;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static  class RSSDataScheduleJob
    { 
        [Obsolete]
        public static void RunJob(string environment)
        {
            
            RecurringJob.AddOrUpdate(() => RSSDataScheduleJob.PullRSSContent(environment), "0 16 * * *");
        }
        public static void PullRSSContent(string environment)
        {
            var rssList = new RSSListUtility().CreateRssList();
            if (rssList.Count > 0)
            {
                foreach (var item in rssList)
                {

                    RssResultDTO rssItem = new RssRunnerService().RunRssGenerator(item.CategoryURL, item.CategoryName, environment);
                    if (rssItem.RssAddedCount > 0)
                    {
                        SourceService sourceService = new SourceService();
                        Source source=sourceService.GetSourceByCategoryLink(item.CategoryURL);
                        Category category = new CategoryService().GetById(rssItem.CategoryId);
                        MailService mailService = new MailService();
                        mailService.SendMailPullRSS(rssItem, source,category, environment);
                    }

                }
            }
        }
    }
}
