using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.IoTDevice.Requests
{
    public class IoTDeviceRequest
    {
        //public int Id { get; set; }
        public string? Name { get; set; }

        public string? Status { get; set; }

        public int? PanelId { get; set; }
    }
}
