using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SourceContentAddOrUpdateDTO
    {
        public string imageURL { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required, Range(0, Double.PositiveInfinity)]
        public int CategoryId { get; set; }
        public string Tags { get; set; }
        public int SourceId { get; set; }
    }
}
