using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class CategoryDTO: DTOAbstractBase
    {
        public CategoryDTO()
        {
            SourceContentsDTO = new List<SourceContentDTO>();
        }
        public string Link { get; set; }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public virtual SourceDTO Source { get; set; }
        public virtual ICollection<SourceContentDTO> SourceContentsDTO { get; set; }
    }
}
