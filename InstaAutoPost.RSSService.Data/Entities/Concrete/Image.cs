using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Entities.Concrete
{
    public class Image:EntityBase,IEntity
    {
        [Required]
        public string ImageLink { get; set; }
        public int SourceContentId { get; set; }
    }
}
