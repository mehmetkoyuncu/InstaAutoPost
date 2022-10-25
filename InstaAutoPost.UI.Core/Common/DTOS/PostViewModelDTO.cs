using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostViewModelDTO
    {
        public int Id { get; set; }
        public PostSocialMediaAccountsCategoryTypeViewModelDTO SocialMediaAccountsCategoryType { get; set; }
        public int OrderNumber { get; set; }
        public PostSourceContentViewModelDTO SourceContent { get; set; }
    }
}
