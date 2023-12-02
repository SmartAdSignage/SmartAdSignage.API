using AutoMapper;
using SmartAdSignage.Core.DTOs.Location.Requests;
using SmartAdSignage.Core.DTOs.Location.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationResponse>().ReverseMap();
            CreateMap<LocationRequest, Location>();
                /*.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.StreetType, opt => opt.MapFrom(src => src.StreetType))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))*/
            /*CreateMap<SmartAdSignage.Core.DTOs.Location.Requests.CreateLocationRequest, SmartAdSignage.Core.Models.Location>();
            CreateMap<SmartAdSignage.Core.DTOs.Location.Requests.UpdateLocationRequest, SmartAdSignage.Core.Models.Location>();*/
        }
    }
}
