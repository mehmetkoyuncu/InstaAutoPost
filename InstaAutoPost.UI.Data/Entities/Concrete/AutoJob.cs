using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
   public class AutoJob:EntityBase, IEntity
    {
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobTimeType { get; set; }
        public bool IsWork { get; set; }
        public string Cron { get; set; }
        public string CronDescription { get; set; }
    }
}
