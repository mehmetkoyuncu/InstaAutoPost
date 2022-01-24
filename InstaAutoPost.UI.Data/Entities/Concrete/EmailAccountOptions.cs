using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.Entities.Concrete
{
    public class EmailAccountOptions : EntityBase, IEntity
    {
        public string AccountMailAddress { get; set; }
        public string AccountMailPassword { get; set; }
    }
}
