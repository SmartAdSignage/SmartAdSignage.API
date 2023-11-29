using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Text { get; set; }
        public string? Html { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
