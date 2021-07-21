using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Entities.Concrete
{
    public class SourceContent: EntityBase, IEntity
    {
        [Required]
        public string Link { get; set; }
        [Required,MinLength(3)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ContentInsertAt  { get; set; }
        public bool SendOutForPost { get; set; }
        public int SourceId { get; set; }
        public ICollection<Image> Images { get; set; }

    }
}
