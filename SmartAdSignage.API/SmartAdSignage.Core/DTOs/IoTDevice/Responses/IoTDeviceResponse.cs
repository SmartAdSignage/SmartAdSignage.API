using SmartAdSignage.Core.DTOs.Panel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.IoTDevice.Responses
{
    public class IoTDeviceResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Status { get; set; }

        public PanelResponse? Panel { get; set; }
    }
}
