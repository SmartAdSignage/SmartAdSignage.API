using SmartAdSignage.Core.DTOs.Panel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Location.Responses
{
    public class LocationResponse
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? StreetType { get; set; }
        public string? BuildingNumber { get; set; }
        public ICollection<PanelResponse>? Panels { get; set; }
    }
}
