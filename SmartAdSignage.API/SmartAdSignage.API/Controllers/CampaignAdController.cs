using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Requests;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignAdController(IMapper mapper, ICampaignAdService campaignAdvertisementService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICampaignAdService _campaignAdvertisementService = campaignAdvertisementService;

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

        [HttpGet]
        [Route("campaign-advertisement/{id}")]
        public async Task<IActionResult> GetCampaignAdvertisement(int id)
        {
            var result = await _campaignAdvertisementService.GetCampaignAdvertisementByIdAsync(id);
            if (result == null)
                return NotFound();
            var adCampaign = _mapper.Map<CampaignAdvertisementResponse>(result);
            return Ok(adCampaign);
        }

        [HttpDelete]
        [Route("campaign-advertisement/{id}")]
        public async Task<IActionResult> DeleteCampaignAdvertisement(int id)
        {
            var result = await _campaignAdvertisementService.DeleteCampaignAdvertisementByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("campaign-advertisement")]
        public async Task<IActionResult> CreateCampaignAdvertisement(CampaignAdvertisementRequest campaignAdvertisementRequest)
        {
            var campaignAdvertisement = _mapper.Map<CampaignAdvertisement>(campaignAdvertisementRequest);
            var result = await _campaignAdvertisementService.CreateCampaignAdvertisementAsync(campaignAdvertisement);
            if (result == null)
                return NotFound();
            var campaignAdvertisementResponse = _mapper.Map<CampaignAdvertisementResponse>(result);
            return Ok(campaignAdvertisementResponse);
        }

        [HttpPut]
        [Route("campaign-advertisement/{id}")]
        public async Task<IActionResult> UpdateCampaignAdvertisement(int id, CampaignAdvertisementRequest campaignAdvertisementRequest)
        {
            var campaignAdvertisement = _mapper.Map<CampaignAdvertisement>(campaignAdvertisementRequest);
            var result = await _campaignAdvertisementService.UpdateCampaignAdvertisementAsync(id, campaignAdvertisement);
            if (result == null)
                return NotFound();
            var campaignAdvertisementResponse = _mapper.Map<CampaignAdvertisementResponse>(result);
            return Ok(campaignAdvertisementResponse);
        }
    }
}
