using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
   public  class SentMailDTO
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string Body { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
    }
}
