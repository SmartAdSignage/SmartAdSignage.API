using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class AdCampaign : BaseEntity
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TargetedViews { get; set; }

        public string? Status { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<CampaignAdvertisement>? CampaignAdvertisements { get; set; }

        public ICollection<Panel>? Panels { get; set; }
    }
}
