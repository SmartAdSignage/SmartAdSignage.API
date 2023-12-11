using AutoMapper;
using SmartAdSignage.Core.DTOs.Queue.Requests;
using SmartAdSignage.Core.DTOs.Queue.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class QueueMappingProfile : Profile
    {
        public QueueMappingProfile()
        {
            CreateMap<Queue, QueueResponse>().ReverseMap();
            CreateMap<QueueRequest, Queue>().ReverseMap();
            CreateMap<Queue, QueuePropsResponse>().ReverseMap();
        }
    }
}
