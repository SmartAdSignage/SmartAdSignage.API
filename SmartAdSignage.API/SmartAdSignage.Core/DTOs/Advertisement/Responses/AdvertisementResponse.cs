using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Core.DTOs.Queue.Responses;
using SmartAdSignage.Core.DTOs.User.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Advertisement.Responses
{
    public class AdvertisementResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public byte[] File { get; set; }

        public UserResponse? User { get; set; }

        public ICollection<CampaignAdvertisementPropsResponse>? CampaignAdvertisements { get; set; }

        public ICollection<QueuePropsResponse>? Queues { get; set; }
    }
}
