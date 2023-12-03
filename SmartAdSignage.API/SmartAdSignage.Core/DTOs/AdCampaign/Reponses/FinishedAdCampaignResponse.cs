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

        public string? UserId { get; set; }
    }
}
