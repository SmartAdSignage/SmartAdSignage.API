using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.DTOs.User.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.AdCampaign.Reponses
{
    public class FinishedAdCampaignResponse
    {
        public int? Id { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TargetedViews { get; set; }

        public int? OverallViews { get; set; }

        public int? OverallDisplays { get; set; }

        public int? AdvertisementsDisplayed { get; set; }

        public string? Status { get; set; }

        public UserResponse? User { get; set; }

        public ICollection<CampaignAdvertisementResponse>? CampaignAdvertisements { get; set; }

        public ICollection<PanelResponse>? Panels { get; set; }
    }
}
