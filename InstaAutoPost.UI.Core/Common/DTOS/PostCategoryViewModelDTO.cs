using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostCategoryViewModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public virtual PostSourceViewModelDTO Source { get; set; }
    }
}
