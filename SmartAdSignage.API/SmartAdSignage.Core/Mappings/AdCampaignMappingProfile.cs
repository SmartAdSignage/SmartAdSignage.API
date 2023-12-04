using AutoMapper;
using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.AdCampaign.Requests;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Mappings
{
    public class AdCampaignMappingProfile : Profile
    {
        public AdCampaignMappingProfile()
        {
            CreateMap<AdCampaign, AdCampaignResponse>();
            CreateMap<AdCampaignRequest, AdCampaign>();
            CreateMap<AdCampaign, FinishedAdCampaignResponse>();
        }
    }
}
