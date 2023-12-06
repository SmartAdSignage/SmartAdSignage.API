using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.AdCampaign.Requests;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Implementations;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdCampaignController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdCampaignService _adCampaignService;
        private readonly Serilog.ILogger _logger;

        public AdCampaignController(IMapper mapper, IAdCampaignService adCampaignService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _adCampaignService = adCampaignService;
            _logger = logger;
        }

        [HttpGet]
        [Route("ad-campaigns")]
        public async Task<IActionResult> GetAdCampaigns([FromQuery] GetRequest adCampaignsRequest)
        {
            var result = await _adCampaignService.GetAllAdCampaignsAsync(adCampaignsRequest.PageInfo);
            if (result.Count() == 0)
            {
                _logger.Error($"Ad campaigns not found");
                return NotFound($"Ad campaigns not found");
            }
            var adCampaigns = _mapper.Map<IEnumerable<AdCampaignResponse>>(result);
            return Ok(adCampaigns);
        }

        [HttpGet]
        [Route("ad-campaigns/{id}")]
        public async Task<IActionResult> GetAdCampaignsByUserId(string id)
        {
            var result = await _adCampaignService.GetAllAdCampaignsByUserIdAsync(id);
            if (result.Count() == 0 || result == null)
            {
                _logger.Error($"Ad campaigns with user id: {id} not found");
                return NotFound($"Ad campaigns with user id: {id} not found");
            }
            var adCampaigns = _mapper.Map<IEnumerable<AdCampaignResponse>>(result);
            return Ok(adCampaigns);
        }

        [HttpGet]
        [Route("ad-campaign/{id}")]
        public async Task<IActionResult> GetAdCampaign(int id)
        {
            /*var userId = User.Claims.FirstOrDefault()?.Value;*/
            var result = await _adCampaignService.GetAdCampaignByIdAsync(id);
            if (result == null)
            {
                _logger.Error($"Ad campaign with id: {id} not found");
                return NotFound($"Ad campaign with id: {id} not found");
            }
            var adCampaign = _mapper.Map<AdCampaignResponse>(result);
            return Ok(adCampaign);
        }

        [HttpDelete]
        [Route("ad-campaign/{id}")]
        public async Task<IActionResult> DeleteAdCampaign(int id)
        {
            var result = await _adCampaignService.DeleteAdCampaignByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"Ad campaign with id: {id} not found");
                return NotFound($"Ad campaign with id: {id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("ad-campaign")]
        public async Task<IActionResult> CreateAdCampaign(AdCampaignRequest adCampaignRequest)
        {
            var adCampaign = _mapper.Map<AdCampaign>(adCampaignRequest);
            var result = await _adCampaignService.CreateAdCampaignAsync(adCampaign);
            if (result == null)
            {
                _logger.Error($"Couldn't create ad campaign");
                return BadRequest($"Couldn't create ad campaign");
            }

            var adCampaignResponse = _mapper.Map<AdCampaignResponse>(result);
            return Ok(adCampaignResponse);
        }

        [HttpPut]
        [Route("ad-campaign/{id}")]
        public async Task<IActionResult> UpdateAdCampaign(int id, AdCampaignRequest adCampaignRequest)
        {
            var adCampaign = _mapper.Map<AdCampaign>(adCampaignRequest);
            var result = await _adCampaignService.UpdateAdCampaignAsync(id, adCampaign);
            if (result == null)
            {
                _logger.Error($"Ad campaign with id: {id} not found");
                return NotFound($"Ad campaign with id: {id} not found");
            }
            var adCampaignResponse = _mapper.Map<AdCampaignResponse>(result);
            return Ok(adCampaignResponse);
        }

        [HttpGet]
        [Route("results/{userId}")]
        public async Task<IActionResult> GetResults(string userId)
        {
            var adCampaigns = await _adCampaignService.GetFinishedAdCampaigns(userId);
            if (adCampaigns.Count() == 0 || adCampaigns == null)
            {
                _logger.Error($"No finished ad campaigns found for user with id: {userId}");
                return NotFound($"No finished ad campaigns found for user with id: {userId}");
            }

            IList<FinishedAdCampaignResponse> results = new List<FinishedAdCampaignResponse>();
            foreach (var adCampaign in adCampaigns)
            {
                var res = _mapper.Map<FinishedAdCampaignResponse>(adCampaign);
                var statistics = await _adCampaignService.GetStatistics(adCampaign);
                res.OverallViews = statistics[0];
                res.OverallDisplays = statistics[1];
                res.AdvertisementsDisplayed = statistics[2];
                results.Add(res);
            }
            return Ok(results);
        }
    }
}
