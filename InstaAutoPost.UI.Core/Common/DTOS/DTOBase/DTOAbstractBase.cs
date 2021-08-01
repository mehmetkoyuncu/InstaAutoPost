using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS.DTOBase
{
   public abstract class DTOAbstractBase
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
