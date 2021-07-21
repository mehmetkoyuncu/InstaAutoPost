using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Entities.Abstract
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
