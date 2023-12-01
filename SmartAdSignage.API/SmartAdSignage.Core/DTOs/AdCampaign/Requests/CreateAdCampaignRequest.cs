using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.AdCampaign.Requests
{
    public class CreateAdCampaignRequest
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TargetedViews { get; set; }

        public string? Status { get; set; }

        public string? UserId { get; set; }
    }
}
