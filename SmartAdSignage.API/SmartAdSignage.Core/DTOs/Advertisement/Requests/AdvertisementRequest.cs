using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Advertisement.Requests
{
    public class AdvertisementRequest
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? File { get; set; }
        public string? UserId { get; set; }
    }
}
