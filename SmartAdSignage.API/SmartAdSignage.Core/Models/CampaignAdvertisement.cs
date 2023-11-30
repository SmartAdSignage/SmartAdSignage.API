using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class CampaignAdvertisement : BaseEntity
    {
        public int Views { get; set; }

        public int DisplayedTimes { get; set; }

        public int? AdCampaignId { get; set; }
        public AdCampaign AdCampaign { get; set; }
        public int? AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
