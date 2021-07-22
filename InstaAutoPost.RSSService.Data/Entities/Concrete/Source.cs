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
            Categories = new List<Category>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Category> Categories { get; set; }


    }
}
