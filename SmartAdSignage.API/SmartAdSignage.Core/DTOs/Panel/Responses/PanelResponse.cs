using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.IoTDevice.Responses;
using SmartAdSignage.Core.DTOs.Location.Responses;
using SmartAdSignage.Core.DTOs.Queue.Responses;
using SmartAdSignage.Core.DTOs.User.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Panel.Responses
{
    public class PanelResponse
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public string? Status { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public LocationResponse? Location { get; set; }

        public UserResponse? User { get; set; }

        public ICollection<AdCampaignResponse>? AdCampaigns { get; set; }

        public ICollection<IoTDeviceResponse>? IoTDevices { get; set; }

        public ICollection<QueueResponse>? Queues { get; set; }
    }
}
