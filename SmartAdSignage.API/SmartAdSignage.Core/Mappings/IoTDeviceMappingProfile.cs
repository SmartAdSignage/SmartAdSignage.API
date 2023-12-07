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
            CreateMap<IoTDevice, IoTDeviceResponse>().ReverseMap();
            CreateMap<IoTDeviceRequest, IoTDevice>().ReverseMap();
            CreateMap<IoTDevice, IoTDevicePropsResponse>().ReverseMap();
        }
    }
}
