using SmartAdSignage.Core.Extra;
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
        private readonly IUnitOfWork _unitOfWork;
        public IoTDeviceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IoTDevice> CreateIoTDeviceAsync(IoTDevice ioTDevice)
        {
            if (ioTDevice == null)
                throw new ArgumentException("Invalid arguments");
            var result = await _unitOfWork.IoTDevices.AddAsync(ioTDevice);
            await _unitOfWork.IoTDevices.SaveAsync();
            return result;
        }

        public async Task<bool> DeleteIoTDeviceByIdAsync(int id)
        {
            var iotDevice = await _unitOfWork.IoTDevices.GetByIdAsync(id);
            var result = _unitOfWork.IoTDevices.Delete(iotDevice);
            await _unitOfWork.IoTDevices.SaveAsync();
            return result;
        }

        public async Task<IEnumerable<IoTDevice>> GetAllIoTDevicesAsync(PageInfo pageInfo)
        {
            return await _unitOfWork.IoTDevices.GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitySelector.IoTDeviceSelector);
        }

        public async Task<IEnumerable<IoTDevice>> GetAllIoTDevicesByPanelIdAsync(int id)
        {
            return await _unitOfWork.IoTDevices.GetByConditionAsync(x => x.PanelId == id, EntitySelector.IoTDeviceSelector);
        }

        public async Task<IoTDevice> GetIoTDeviceByIdAsync(int id)
        {
            var result = await _unitOfWork.IoTDevices.GetByConditionAsync(x => x.Id == id, EntitySelector.IoTDeviceSelector);
            return result.FirstOrDefault();
        }

        public async Task<IoTDevice> UpdateIoTDeviceAsync(int id, IoTDevice ioTDevice)
        {
            if (ioTDevice == null)
                throw new ArgumentException("Invalid arguments");
            var existingIoTDevice = await _unitOfWork.IoTDevices.GetByIdAsync(id);
            if (existingIoTDevice == null)
                return null;
            existingIoTDevice.Name = ioTDevice.Name;
            existingIoTDevice.Status = ioTDevice.Status;
            existingIoTDevice.PanelId = ioTDevice.PanelId;
            existingIoTDevice.DateUpdated = DateTime.Now;
            var result = _unitOfWork.IoTDevices.Update(existingIoTDevice);
            await _unitOfWork.IoTDevices.SaveAsync();
            return result;
        }
    }
}
