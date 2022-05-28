using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.Constants
{
    public class HangfireIDConstants
    {
        public const string CreateFolder = "CreateFolderScheduleJob.CreateFolder";
        public const string PublishPost = "PublishPostScheduleJob.PublishPost";
        public const string RemoveContentCreatedFolder = "RemoveContentCreatedFolderScheduleJob.RemoveContentCreatedFolder";
        public const string RemoveContentPublished = "RemoveContentPublishedScheduleJob.RemoveContentPublished";
        public const string RemoveSentMails = "RemoveSentMailsScheduleJob.RemoveSentMails";
        public const string PullRSSContent = "PullRSSDataScheduleJob.PullRSSContent";
    }
}
