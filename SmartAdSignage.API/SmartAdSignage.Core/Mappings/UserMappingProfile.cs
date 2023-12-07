using AutoMapper;
using SmartAdSignage.Core.DTOs.User.Requests;
using SmartAdSignage.Core.DTOs.User.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<LoginRequest, User>().ReverseMap();

            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
            CreateMap<User, RegisteredUserResponse>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<UpdateUserRequest, User>().ReverseMap();
        }        
    }
}
