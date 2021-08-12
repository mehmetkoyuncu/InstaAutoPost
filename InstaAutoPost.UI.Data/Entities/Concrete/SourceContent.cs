using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class SourceContent : EntityBase, IEntity
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ContentInsertAt { get; set; }
        public bool SendOutForPost { get; set; }
        public int CategoryId { get; set; }
        public string SourceContentId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
