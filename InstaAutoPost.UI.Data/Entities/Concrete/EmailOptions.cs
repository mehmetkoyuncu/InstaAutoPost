using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class EmailOptions:EntityBase,IEntity
    {
        public string MailDefaultHTMLContent { get; set; }
        public string MailDefaultContent { get; set; }
        public string MailDefaultSubject { get; set; }
        public string MailDefaultTo { get; set; }
    }
}
