using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SourceWithCategoryCountDTO:DTOAbstractBase
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int CatrgoryCount { get; set; }
    }
}
