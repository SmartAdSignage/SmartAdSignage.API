using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Requests;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignAdController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICampaignAdService _campaignAdvertisementService;
        private readonly Serilog.ILogger _logger;

        public CampaignAdController(IMapper mapper, ICampaignAdService campaignAdvertisementService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _campaignAdvertisementService = campaignAdvertisementService;
            _logger = logger;
        }

        [HttpGet]
        [Route("campaign-advertisements")]
        public async Task<IActionResult> GetCampaignAdvertisements([FromQuery] GetRequest getRequest)
        {
            var result = await _campaignAdvertisementService.GetAllCampaignAdvertisementsAsync(getRequest.PageInfo);
            if (!result.Any() || result == null) 
            {
                _logger.Error("No campaign advertisements found");
                return NotFound("No campaign advertisements found");
            }
            var adCampaigns = _mapper.Map<IEnumerable<CampaignAdvertisementResponse>>(result);
            return Ok(adCampaigns);
        }

        [HttpGet]
        [Route("campaign-advertisement/{id}")]
        public async Task<IActionResult> GetCampaignAdvertisement(int id)
        {
            var result = await _campaignAdvertisementService.GetCampaignAdvertisementByIdAsync(id);
            if (result == null) 
            {
                _logger.Error($"Campaign advertisement with id:{id} not found");
                return NotFound($"Campaign advertisement with id:{id} not found");
            }
            var adCampaign = _mapper.Map<CampaignAdvertisementResponse>(result);
            return Ok(adCampaign);
        }

        [HttpGet]
        [Route("campaign-advertisements/{campaignId}")]
        public async Task<IActionResult> GetCampaignAdvertisementsByAdcampaignId(int campaignId)
        {
            var result = await _campaignAdvertisementService.GetAllCampaignAdvertisementsByCampaignIdAsync(campaignId);
            if (!result.Any() || result == null)
            {
                _logger.Error($"Campaign advertisements with campaign id:{campaignId} not found");
                return NotFound($"Campaign advertisements with campaign id:{campaignId} not found");
            }
            var adCampaigns = _mapper.Map<IEnumerable<CampaignAdvertisementResponse>>(result);
            return Ok(adCampaigns);
        }

        [HttpDelete]
        [Route("campaign-advertisement/{id}")]
        public async Task<IActionResult> DeleteCampaignAdvertisement(int id)
        {
            var result = await _campaignAdvertisementService.DeleteCampaignAdvertisementByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"Campaign advertisement with id: {id} not found");
                return NotFound($"Campaign advertisement with id: {id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("campaign-advertisement")]
        public async Task<IActionResult> CreateCampaignAdvertisement(CampaignAdvertisementRequest campaignAdvertisementRequest)
        {
            var campaignAdvertisement = _mapper.Map<CampaignAdvertisement>(campaignAdvertisementRequest);
            var result = await _campaignAdvertisementService.CreateCampaignAdvertisementAsync(campaignAdvertisement);
            if (result == null)
            {
                _logger.Error($"Couldn't create campaign advertisement");
                return BadRequest($"Couldn't create campaign advertisement");
            }
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
            {
                _logger.Error($"Campaign advertisement with id: {id} not found");
                return NotFound($"Campaign advertisement with id: {id} not found");
            }
            var campaignAdvertisementResponse = _mapper.Map<CampaignAdvertisementResponse>(result);
            return Ok(campaignAdvertisementResponse);
        }
    }
}
