using SmartAdSignage.Core.Models;
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
        Task<IoTDevice> GetIoTDeviceByIdAsync(int id);

        Task<bool> DeleteIoTDeviceByIdAsync(int id);

        Task<IoTDevice> UpdateIoTDeviceAsync(int id, IoTDevice ioTDevice);
    }
}
