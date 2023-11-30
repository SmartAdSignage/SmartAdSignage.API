using AutoMapper;
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
            CreateMap<CampaignAdvertisement, CampaignAdvertisementResponse>();
            /*CreateMap<SmartAdSignage.Core.DTOs.CampaignAdvertisement.Requests.CreateCampaignAdvertisementRequest, SmartAdSignage.Core.Models.CampaignAdvertisement>();
            CreateMap<SmartAdSignage.Core.DTOs.CampaignAdvertisement.Requests.UpdateCampaignAdvertisementRequest, SmartAdSignage.Core.Models.CampaignAdvertisement>();
            CreateMap<SmartAdSignage.Core.Models.CampaignAdvertisement, SmartAdSignage.Core.DTOs.CampaignAdvertisement.Requests.UpdateCampaignAdvertisementRequest>();*/
        }
    }
}
