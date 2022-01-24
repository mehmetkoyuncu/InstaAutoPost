using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class Email:EntityBase,IEntity
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string Body { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
    }
}
