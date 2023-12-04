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
        public virtual Location? Location { get; set; }
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<AdCampaign>? AdCampaigns { get; set; }
        public virtual ICollection<IoTDevice>? IoTDevices { get; set; }
        public virtual ICollection<Queue>? Queues { get; set; }
    }
}
