using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.AdCampaign.Responses;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses
{
    public class CampaignAdvertisementResponse
    {
        public int Id { get; set; }
        public int Views { get; set; }

        public int DisplayedTimes { get; set; }

        public AdCampaignPropsResponse AdCampaign { get; set; }

        public AdvertisementPropsResponse Advertisement { get; set; }
    }
}
