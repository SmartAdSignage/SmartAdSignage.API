using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class IoTDeviceService : IIoTDeviceService
    {
        private readonly IGenericRepository<IoTDevice> _genericRepository;

        public IoTDeviceService(IGenericRepository<IoTDevice> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task<IoTDevice> CreateIoTDeviceAsync(IoTDevice ioTDevice)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<IoTDevice>> GetAllIoTDevicesAsync()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
