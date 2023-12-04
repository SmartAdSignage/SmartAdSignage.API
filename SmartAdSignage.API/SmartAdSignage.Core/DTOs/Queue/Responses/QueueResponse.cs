using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Queue.Responses
{
    public class QueueResponse
    {
        public int Id { get; set; }
        public PanelResponse? Panel { get; set; }
        public AdvertisementResponse? Advertisement { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
