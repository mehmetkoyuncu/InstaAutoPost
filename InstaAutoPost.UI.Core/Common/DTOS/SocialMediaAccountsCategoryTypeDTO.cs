using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SocialMediaAccountsCategoryTypeDTO : DTOAbstractBase
    {
        public int SocialMediaAccountId { get; set; }
        public int CategoryTypeId { get; set; }
        public SocialMediaDTO SocialMediaAccounts { get; set; }
        public CategoryTypeDTO CategoryType { get; set; }
    }
}
