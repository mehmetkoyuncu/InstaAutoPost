using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.DTOS
{
    public class PostSourceContentViewModelDTO
    {
        public int Id { get; set; }
        public string imageURL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime ContentInsertAt { get; set; }
        public PostCategoryViewModelDTO Category { get; set; }
    }
}
