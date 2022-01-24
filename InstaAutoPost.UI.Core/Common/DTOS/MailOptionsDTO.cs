using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class MailOptionsDTO:DTOAbstractBase
    {
        public string MailDefaultHTMLContent { get; set; }
        public string MailDefaultSubject { get; set; }
        public string MailDefaultTo { get; set; }
    }
}
