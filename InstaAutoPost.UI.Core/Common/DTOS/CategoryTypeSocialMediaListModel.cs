using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class CategoryTypeSocialMediaListModel
    {
        public CategoryTypeSocialMediaListModel()
        {
            CategoryTypes = new List<CategoryTypeDTO>();
            SocialMedias = new List<SocialMediaDTO>();
        }
        public List<CategoryTypeDTO> CategoryTypes { get; set; }
        public List<SocialMediaDTO> SocialMedias { get; set; }
    }
}
