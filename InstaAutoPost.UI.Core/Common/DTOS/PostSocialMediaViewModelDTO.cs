using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostSocialMediaViewModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNameOrMail { get; set; }
        public string Icon { get; set; }
    }
}
