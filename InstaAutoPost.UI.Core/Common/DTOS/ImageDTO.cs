using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class ImageDTO: DTOAbstractBase
    {
        public string ImageLink { get; set; }
        public int SourceContentId { get; set; }
        public virtual SourceContentDTO SourceContent { get; set; }
    }
}
