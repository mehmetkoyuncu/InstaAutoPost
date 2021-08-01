using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SourceContentDTO: DTOAbstractBase
    {
        public SourceContentDTO()
        {
            ImagesDTO = new List<ImageDTO>();
        }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ContentInsertAt { get; set; }
        public bool SendOutForPost { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<ImageDTO> ImagesDTO { get; set; }
    }
}
