using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignAdController(IMapper mapper, ICampaignAdvertisementService campaignAdvertisementService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICampaignAdvertisementService _campaignAdvertisementService = campaignAdvertisementService;

        [HttpGet]
        [Route("campaign-advertisements")]
        public async Task<IActionResult> GetCampaignAdvertisements()
        {
            var result = await _campaignAdvertisementService.GetAllCampaignAdvertisementsAsync();
            if (result.Count() == 0)
                return NotFound();
            var adCampaigns = _mapper.Map<IEnumerable<CampaignAdvertisementResponse>>(result);
            return Ok(adCampaigns);
        }
    }
}
