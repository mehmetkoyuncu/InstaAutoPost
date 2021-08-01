using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SourceDTO: DTOAbstractBase
    {
        public SourceDTO()
        {
            CategoriesDTO = new List<CategoryDTO>();
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<CategoryDTO> CategoriesDTO { get; set; }
    }
}
