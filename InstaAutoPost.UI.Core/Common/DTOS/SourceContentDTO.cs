using InstaAutoPost.UI.Core.Common.DTOS.DTOBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class SourceContentDTO
    {
        public int Id { get; set; }
        public string imageURL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ContentInsertAt { get; set; }
        public bool SendOutForPost { get; set; }
        public string Tags { get; set; }
        public string CategoryName { get; set; }
        public string SourceName { get; set; }
        public int CategoryId { get; set; }
        public int  SourceId { get; set; }
        public bool IsCreatedFolder { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime? PostTime { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
