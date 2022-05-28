using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class AutoJobDTO
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobTimeType { get; set; }
        public bool IsWork { get; set; }
        public string CronDescription { get; set; }
        public string Cron { get; set; }
    }
}
