using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Panel.Responses
{
    public class PanelResponse
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public string? Status { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? LocationId { get; set; }
        public string? UserId { get; set; }
    }
}
