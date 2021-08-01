using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class Source : EntityBase, IEntity
    {
        public Source()
        {
            Categories = new List<Category>();
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Category> Categories { get; set; }


    }
}
