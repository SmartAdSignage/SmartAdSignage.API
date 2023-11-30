using AutoMapper;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class AdvertisementMappingProfile : Profile
    {
        public AdvertisementMappingProfile()
        {
            CreateMap<Advertisement, AdvertisementResponse>();
        }
    }
}
