﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.IoTDevice.Responses
{
    public class IoTDevicePropsResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Status { get; set; }
    }
}
