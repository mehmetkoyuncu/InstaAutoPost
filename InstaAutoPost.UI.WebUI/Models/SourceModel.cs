using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Models
{
    public class SourceModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
