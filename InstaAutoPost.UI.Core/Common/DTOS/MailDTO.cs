using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class MailDTO:DTOAbstractBase
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string Body { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
    }
}
