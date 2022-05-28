using Hangfire;
using Hangfire.Storage;
using InstaAutoPost.UI.Core.Common.Constants;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs.Main
{
    public class ScheduleJobRunner
    {
        [Obsolete]
        public void RunJobs(string environment)
        {
            try
            {
                var jobList = new AutoJobService().GetAutoJobs();
                foreach (var item in jobList)
                {
                    // var instagramBot = new SeleniumMain().Publish(content,environment);
                    if (item.IsWork)
                    {
                        switch (item.JobName)
                        {
                            case JobNamesConstants.CreateFolder:
                                CreateFolderScheduleJob.RunJob(environment, item.Cron);
                                break;
                            case JobNamesConstants.PublishContent:
                                PublishPostScheduleJob.RunJob(environment, item.Cron);
                                break;
                            case JobNamesConstants.PullRSSContent:
                                PullRSSDataScheduleJob.RunJob(environment, item.Cron);
                                break;
                            case JobNamesConstants.RemoveContentCreatedFolder:
                                RemoveContentCreatedFolderScheduleJob.RunJob(environment, item.Cron);
                                break;
                            case JobNamesConstants.RemoveContentPublished:
                                RemoveContentPublishedScheduleJob.RunJob(environment,item.Cron);
                                break;
                            case JobNamesConstants.RemoveSentMails:
                                RemoveSentMailsScheduleJob.RunJob(item.Cron);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (!item.IsWork)
                    {
                        switch (item.JobName)
                        {
                            case JobNamesConstants.CreateFolder:
                                RemoveJob(HangfireIDConstants.CreateFolder);
                                break;
                            case JobNamesConstants.PublishContent:
                                RemoveJob(HangfireIDConstants.PublishPost);
                                break;
                            case JobNamesConstants.PullRSSContent:
                                RemoveJob(HangfireIDConstants.PullRSSContent);
                                break;
                            case JobNamesConstants.RemoveContentCreatedFolder:
                                RemoveJob(HangfireIDConstants.RemoveContentCreatedFolder);
                                break;
                            case JobNamesConstants.RemoveContentPublished:
                                RemoveJob(HangfireIDConstants.RemoveContentPublished);
                                break;
                            case JobNamesConstants.RemoveSentMails:
                                RemoveJob(HangfireIDConstants.RemoveSentMails);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Job işlemlerinde bir hata oluştu. - {exMessage}");
            }

        }
        public void RemoveJob(string hangfireId)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                var job = connection.GetRecurringJobs().FirstOrDefault(x => x.Id == hangfireId);
                if (job != null)
                    BackgroundJob.Delete(hangfireId);
            }
        }
        public void RemoveAll()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in StorageConnectionExtensions.GetRecurringJobs(connection))
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }

    }
}
