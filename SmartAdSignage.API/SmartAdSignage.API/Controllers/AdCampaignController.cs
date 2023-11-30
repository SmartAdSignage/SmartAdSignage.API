using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Services.Services.Implementations;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdCampaignController(IMapper mapper, IAdCampaignService adCampaignService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAdCampaignService _adCampaignService = adCampaignService;

        [HttpGet]
        [Route("ad-campaigns")]
        public async Task<IActionResult> GetAdCampaigns()
        {
            var result = await _adCampaignService.GetAllAdCampaignsAsync();
            if (result.Count() == 0)
                return NotFound();
            var adCampaigns = _mapper.Map<IEnumerable<AdCampaignResponse>>(result);
            return Ok(adCampaigns);
        }
    }
}
