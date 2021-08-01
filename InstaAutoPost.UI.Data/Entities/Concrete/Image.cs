using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class Image : EntityBase, IEntity
    {
        public string ImageLink { get; set; }
        public int SourceContentId { get; set; }
        public virtual SourceContent SourceContent { get; set; }
    }
}
