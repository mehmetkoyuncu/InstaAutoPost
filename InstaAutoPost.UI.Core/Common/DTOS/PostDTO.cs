using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostDTO:DTOAbstractBase
    {
        public SocialMediaAccountsCategoryTypeDTO SocialMediaAccountsCategoryType { get; set; }
        public int SocialMediaAccountsCategoryTypeId { get; set; }
        public int ContentId { get; set; }
        public int OrderNumber { get; set; }
        public bool IsSpecialPost { get; set; }
    }
}
