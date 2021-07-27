using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Models
{
    public class CategoryModel
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public int Id { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        SourceModel SourceModel { get; set; }
        public int SouurceModelId { get; set; }
    }
}
