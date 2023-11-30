using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class Panel : BaseEntity
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public string? Status { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<AdCampaign>? AdCampaigns { get; set; }
        public ICollection<IoTDevice>? IoTDevices { get; set; }
        public ICollection<Queue>? Queues { get; set; }
    }
}
