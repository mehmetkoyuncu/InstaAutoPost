using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SelectboxDataDTO
    {
        public List<SelectBoxCategoryDTO> Categories { get; set; }
        public List<SelectboxSourceDTO> Sources { get; set; }
    }
}
