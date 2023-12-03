using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Models
{
    public class AdCampaignPanel : BaseEntity
    {
        public int? AdCampaignId { get; set; }
        public AdCampaign? AdCampaign { get; set; }
        public int? PanelId { get; set; }
        public Panel? Panel { get; set; }
        public int PanelViews { get; set; }
        public int AdvertisementsDisplayed { get; set; }
    }
}
