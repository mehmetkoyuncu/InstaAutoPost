using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Entities.Concrete
{
    public class Category:EntityBase,IEntity
    {
        public Category()
        {
            SourceContents = new List<SourceContent>();
        }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public virtual Source Source { get; set; }
        public virtual ICollection<SourceContent> SourceContents { get; set; }
    }
}
