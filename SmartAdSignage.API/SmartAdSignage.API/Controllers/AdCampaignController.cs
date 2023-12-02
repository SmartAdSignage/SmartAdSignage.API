using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.AdCampaign.Requests;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Core.Models;
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

        [HttpGet]
        [Route("ad-campaign/{id}")]
        public async Task<IActionResult> GetAdCampaign(int id)
        {
            var result = await _adCampaignService.GetAdCampaignByIdAsync(id);
            if (result == null)
                return NotFound();
            var adCampaign = _mapper.Map<AdCampaignResponse>(result);
            return Ok(adCampaign);
        }

        [HttpDelete]
        [Route("ad-campaign/{id}")]
        public async Task<IActionResult> DeleteAdCampaign(int id)
        {
            var result = await _adCampaignService.DeleteAdCampaignByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("ad-campaign")]
        public async Task<IActionResult> CreateAdCampaign(AdCampaignRequest adCampaignRequest)
        {
            var adCampaign = _mapper.Map<AdCampaign>(adCampaignRequest);
            var result = await _adCampaignService.CreateAdCampaignAsync(adCampaign);
            if (result == null)
                return BadRequest();
            var adCampaignResponse = _mapper.Map<AdCampaignResponse>(result);
            return Ok(adCampaignResponse);
        }

        [HttpPut]
        [Route("ad-campaign/{id}")]
        public async Task<IActionResult> UpdateAdCampaign(int id, AdCampaignRequest adCampaignRequest)
        {
            var adCampaign = _mapper.Map<AdCampaign>(adCampaignRequest);
            var result = _adCampaignService.UpdateAdCampaignAsync(id, adCampaign);
            if (result == null)
                return NotFound();
            var adCampaignResponse = _mapper.Map<AdCampaignResponse>(result);
            return Ok(adCampaignResponse);
        }
    }
}
