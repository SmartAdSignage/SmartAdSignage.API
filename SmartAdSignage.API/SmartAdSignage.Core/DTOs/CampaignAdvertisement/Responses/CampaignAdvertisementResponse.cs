using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses
{
    public class CampaignAdvertisementResponse
    {
        public int Views { get; set; }

        public int DisplayedTimes { get; set; }

        public int? AdCampaignId { get; set; }

        public int? AdvertisementId { get; set; }
    }
}
