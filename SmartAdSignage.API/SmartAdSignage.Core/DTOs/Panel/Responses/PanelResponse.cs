using SmartAdSignage.Core.DTOs.AdCampaign.Responses;
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

        public LocationPropsResponse? Location { get; set; }

        public UserResponse? User { get; set; }

        public ICollection<AdCampaignPropsResponse>? AdCampaigns { get; set; }

        public ICollection<IoTDevicePropsResponse>? IoTDevices { get; set; }

        public ICollection<QueuePropsResponse>? Queues { get; set; }
    }
}
