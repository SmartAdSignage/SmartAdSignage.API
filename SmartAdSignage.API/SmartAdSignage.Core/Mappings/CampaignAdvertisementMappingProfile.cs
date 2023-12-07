using AutoMapper;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Requests;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class CampaignAdvertisementMappingProfile : Profile
    {
        public CampaignAdvertisementMappingProfile()
        {
            CreateMap<CampaignAdvertisement, CampaignAdvertisementResponse>().ReverseMap();
            CreateMap<CampaignAdvertisement, CampaignAdvertisementPropsResponse>().ReverseMap();
            CreateMap<CampaignAdvertisementRequest, CampaignAdvertisement>().ReverseMap();
        }
    }
}
