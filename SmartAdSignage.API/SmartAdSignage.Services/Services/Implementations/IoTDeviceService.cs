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
            var result = await _unitOfWork.IoTDevices.AddAsync(ioTDevice);
            await _unitOfWork.IoTDevices.Commit();
            return result;
        }

        public async Task<bool> DeleteIoTDeviceByIdAsync(int id)
        {
            var iotDevice = await _unitOfWork.IoTDevices.GetByIdAsync(id);
            var result = _unitOfWork.IoTDevices.DeleteAsync(iotDevice);
            await _unitOfWork.IoTDevices.Commit();
            return result;
        }

        public async Task<IEnumerable<IoTDevice>> GetAllIoTDevicesAsync()
        {
            return await _unitOfWork.IoTDevices.GetAllAsync();
        }

        public Task<IoTDevice> GetIoTDeviceByIdAsync(int id)
        {
            return _unitOfWork.IoTDevices.GetByIdAsync(id);
        }

        public async Task<IoTDevice> UpdateIoTDeviceAsync(int id, IoTDevice ioTDevice)
        {
            var existingIoTDevice = await _unitOfWork.IoTDevices.GetByIdAsync(id);
            existingIoTDevice.Name = ioTDevice.Name;
            existingIoTDevice.Status = ioTDevice.Status;
            existingIoTDevice.PanelId = ioTDevice.PanelId;
            existingIoTDevice.DateUpdated = DateTime.Now;
            var result = _unitOfWork.IoTDevices.UpdateAsync(existingIoTDevice);
            await _unitOfWork.IoTDevices.Commit();
            return result;
        }
    }
}
