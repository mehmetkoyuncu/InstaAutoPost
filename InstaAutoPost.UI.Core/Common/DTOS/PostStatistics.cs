using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostStatistics
    {
        public int TotalSourceContent { get; set; }
        public int SendedPostCountToday { get; set; }
        public int SendedPostCount { get; set; }
        public int NotSendedPostCount { get; set; }
        public Dictionary<string, int> SocialMediaCountToday {get;set;}
        public Dictionary<string, int> SocialMediaCount { get; set; }
        public List<string> LastPublishedContents { get; set; }
    }
}
