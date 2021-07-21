using InstaAutoPost.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.Data.Entities
{
    public class DataSource:EntityBase,IEntity
    {
        public string Name { get; set; }
        public int URL { get; set; }

    }
}
