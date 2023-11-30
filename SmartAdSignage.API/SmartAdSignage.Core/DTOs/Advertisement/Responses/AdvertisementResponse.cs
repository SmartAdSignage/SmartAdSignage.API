using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Advertisement.Responses
{
    public class AdvertisementResponse
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public byte[] File { get; set; }
        public string? UserId { get; set; }
    }
}
