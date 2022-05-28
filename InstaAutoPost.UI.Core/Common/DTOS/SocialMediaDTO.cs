using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SocialMediaDTO : DTOAbstractBase
    {
        public string Name { get; set; }
        public string AccountNameOrMail { get; set; }
        public string Password { get; set; }
        public string Icon { get; set; }
    }
}
