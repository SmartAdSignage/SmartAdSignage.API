using AutoMapper;
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
            CreateMap<Location, LocationResponse>();
            /*CreateMap<SmartAdSignage.Core.DTOs.Location.Requests.CreateLocationRequest, SmartAdSignage.Core.Models.Location>();
            CreateMap<SmartAdSignage.Core.DTOs.Location.Requests.UpdateLocationRequest, SmartAdSignage.Core.Models.Location>();*/
        }
    }
}
