using Hangfire;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Core.Utilities;
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
            foreach (var item in rssList)
            {
                var rssItem = new RssRunnerService().RunRssGenerator(item.CategoryURL, item.CategoryName, environment);
            }
        }
    }
}
