using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class CategoryType : EntityBase, IEntity
    {
        public CategoryType()
        {
            Category = new List<Category>();
            SocialMediaAccountsCategoryTypes = new List<SocialMediaAccountsCategoryType>();
        }
        public string Name { get; set; }
        public ICollection<Category> Category { get; set; }
        public string Template { get; set; }
        public ICollection<SocialMediaAccountsCategoryType> SocialMediaAccountsCategoryTypes { get; set; }
    }
}
