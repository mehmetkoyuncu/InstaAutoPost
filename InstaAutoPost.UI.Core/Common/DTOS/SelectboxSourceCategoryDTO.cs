using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SelectboxSourceCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
    }
}
