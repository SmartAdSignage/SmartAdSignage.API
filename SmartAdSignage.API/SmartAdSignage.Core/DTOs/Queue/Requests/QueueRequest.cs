using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Queue.Requests
{
    public class QueueRequest
    {
        public int? PanelId { get; set; }
        public int? AdvertisementId { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
