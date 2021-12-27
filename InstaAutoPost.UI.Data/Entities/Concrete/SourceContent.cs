using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class SourceContent : EntityBase, IEntity
    {
        public string imageURL { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ContentInsertAt { get; set; }
        public bool SendOutForPost { get; set; }
        [Required, Range(0, Double.PositiveInfinity)]
        public int CategoryId { get; set; }
        public string SourceContentId { get; set; }
        public string Tags { get; set; }
        public bool IsCreatedFolder { get; set; }
        public virtual Category Category { get; set; }
        

    }
}
