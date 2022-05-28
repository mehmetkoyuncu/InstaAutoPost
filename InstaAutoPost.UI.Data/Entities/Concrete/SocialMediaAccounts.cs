using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class SocialMediaAccounts:EntityBase,IEntity
    {
        public SocialMediaAccounts()
        {
            SocialMediaAccountsCategoryType = new List<SocialMediaAccountsCategoryType>();
        }
        public string Name { get; set; }
        public string AccountNameOrMail { get; set; }
        public string Password { get; set; }
        public string Icon { get; set; }
        public ICollection<SocialMediaAccountsCategoryType> SocialMediaAccountsCategoryType { get; set; }

    }
}
