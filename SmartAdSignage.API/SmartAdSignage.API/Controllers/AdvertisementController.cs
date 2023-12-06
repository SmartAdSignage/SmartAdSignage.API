using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SmartAdSignage.Core.DTOs.Advertisement.Requests;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementService _advertisementService;
        private readonly Serilog.ILogger _logger;

        public AdvertisementController(IMapper mapper, IAdvertisementService advertisementService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _advertisementService = advertisementService;
            _logger = logger;
        }

        [HttpGet]
        [Route("advertisements")]
        public async Task<IActionResult> GetAdvertisements([FromQuery] GetRequest getRequest)
        {
            var result = await _advertisementService.GetAllAdvertisements(getRequest.PageInfo);
            if (result.Count() == 0 || result == null)
            {
                _logger.Information("Advertisements not found");
                return NotFound("Advertisements not found");
            }
            var advertisements = _mapper.Map<IEnumerable<AdvertisementResponse>>(result);
            return Ok(advertisements);
        }

        [HttpGet]
        [Route("advertisement/{id}")]
        public async Task<IActionResult> GetAdvertisement(int id)
        {
            var result = await _advertisementService.GetAdvertisementByIdAsync(id);
            if (result == null)
            {
                _logger.Information($"Advertisement with id:{id} not found");
                return NotFound($"Advertisement with id:{id} not found");
            }
            var advertisement = _mapper.Map<AdvertisementResponse>(result);
            return Ok(advertisement);
        }

        [HttpGet]
        [Route("advertisements/{id}")]
        public async Task<IActionResult> GetAdvertisementsByUserId(string id)
        {
            var result = await _advertisementService.GetAllAdvertisementsByUserIdAsync(id);
            if (result.Count() == 0)
            {
                _logger.Error($"Advertisements with user id: {id} not found");
                return NotFound($"Advertisements with user id: {id} not found");
            }
            var advertisements = _mapper.Map<IEnumerable<AdvertisementResponse>>(result);
            return Ok(advertisements);
        }

        [HttpDelete]
        [Route("advertisement/{id}")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            var result = await _advertisementService.DeleteAdvertisementByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"Advertisement with id: {id} not found");
                return NotFound($"Advertisement with id: {id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("advertisement")]
        public async Task<IActionResult> CreateAdvertisement(AdvertisementRequest advertisementRequest)
        {
            var advertisement = _mapper.Map<Advertisement>(advertisementRequest);
            var result = await _advertisementService.CreateAdvertisementAsync(advertisement);
            if (result == null)
            {
                _logger.Error($"Couldn't create advertisement");
                return BadRequest($"Couldn't create advertisement");
            }
            var advertisementResponse = _mapper.Map<AdvertisementResponse>(result);
            return Ok(advertisementResponse);
        }

        [HttpPut]
        [Route("advertisement/{id}")]
        public async Task<IActionResult> UpdateAdvertisement(int id, AdvertisementRequest advertisementRequest)
        {
            var advertisement = _mapper.Map<Advertisement>(advertisementRequest);
            var result = await _advertisementService.UpdateAdvertisementAsync(id, advertisement);
            if (result == null)
            {
                _logger.Error($"Advertisement with id: {id} not found");
                return NotFound($"Advertisement with id: {id} not found");
            }
            var advertisementResponse = _mapper.Map<AdvertisementResponse>(result);
            return Ok(advertisementResponse);
        }
    }
}
