using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SourceAddOrUpdateDTO
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
