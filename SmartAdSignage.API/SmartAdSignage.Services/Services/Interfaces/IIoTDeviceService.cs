﻿using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IIoTDeviceService
    {
        Task<IoTDevice> CreateIoTDeviceAsync(IoTDevice ioTDevice);
        Task<IEnumerable<IoTDevice>> GetAllIoTDevicesAsync();
    }
}
