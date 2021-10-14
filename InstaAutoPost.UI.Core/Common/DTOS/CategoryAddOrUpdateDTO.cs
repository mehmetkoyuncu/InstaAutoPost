using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class CategoryAddOrUpdateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required, Range(0, Double.PositiveInfinity)]
        public int SourceId { get; set; }
        public string Tags { get; set; }
    }
}
