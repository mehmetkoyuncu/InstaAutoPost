using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class Category : EntityBase, IEntity
    {
        public Category()
        {
            SourceContents = new List<SourceContent>();
        }
        public string Link { get; set; }
        [Required]
        public string Name { get; set; }
        [Required,Range(0,Double.PositiveInfinity)]
        public int SourceId { get; set; }
        public virtual Source Source { get; set; }
        public string Tags { get; set; }
        public virtual ICollection<SourceContent> SourceContents { get; set; }
    }
}
