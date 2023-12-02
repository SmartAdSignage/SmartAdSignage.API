using AutoMapper;
using SmartAdSignage.Core.DTOs.IoTDevice.Requests;
using SmartAdSignage.Core.DTOs.IoTDevice.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class IoTDeviceMappingProfile : Profile
    {
        public IoTDeviceMappingProfile()
        {
            CreateMap<IoTDevice, IoTDeviceResponse>();
            CreateMap<IoTDeviceRequest, IoTDevice>();
            /*CreateMap<SmartAdSignage.Core.DTOs.IoTDevice.Requests.CreateIoTDeviceRequest, SmartAdSignage.Core.Models.IoTDevice>();
            CreateMap<SmartAdSignage.Core.DTOs.IoTDevice.Requests.UpdateIoTDeviceRequest, SmartAdSignage.Core.Models.IoTDevice>();
            CreateMap<SmartAdSignage.Core.Models.IoTDevice, SmartAdSignage.Core.DTOs.IoTDevice.Requests.UpdateIoTDeviceRequest>();*/
        }
    }
}
