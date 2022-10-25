using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostSocialMediaAccountsCategoryTypeViewModelDTO
    {
        public int Id { get; set; }
        public PostSocialMediaViewModelDTO SocialMediaAccounts { get; set; }
    }
}
