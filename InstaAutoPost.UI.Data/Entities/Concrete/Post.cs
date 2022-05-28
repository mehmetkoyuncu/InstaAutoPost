using InstaAutoPost.UI.Data.Entities.Abstract;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class Post:EntityBase,IEntity
    {
        public SocialMediaAccountsCategoryType SocialMediaAccountsCategoryType { get; set; }
        public int SocialMediaAccountsCategoryTypeId { get; set; }
        public int ContentId { get; set; }
        public int OrderNumber { get; set; }
        public bool IsSpecialPost { get; set; }
    }
}
