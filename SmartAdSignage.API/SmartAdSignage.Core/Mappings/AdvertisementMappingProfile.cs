using AutoMapper;
using SmartAdSignage.Core.DTOs.Advertisement.Requests;
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
            CreateMap<AdvertisementRequest, Advertisement>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => File.ReadAllBytes(src.File)));
        }
    }
}
