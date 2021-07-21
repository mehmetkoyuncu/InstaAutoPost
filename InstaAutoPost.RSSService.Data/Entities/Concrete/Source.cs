using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Entities.Concrete
{
    public class Source : EntityBase, IEntity
    {
        public Source()
        {
            SourceContents = new List<SourceContent>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
        public string Image { get; set; }
        public ICollection<SourceContent> SourceContents { get; set; }



    }
}
