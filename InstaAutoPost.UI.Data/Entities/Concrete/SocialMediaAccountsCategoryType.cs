using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class SocialMediaAccountsCategoryType:EntityBase,IEntity
    {
        public SocialMediaAccountsCategoryType()
        {
            Posts = new List<Post>();
        }
        public int SocialMediaAccountId { get; set; }
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; }
        public SocialMediaAccounts SocialMediaAccounts { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
