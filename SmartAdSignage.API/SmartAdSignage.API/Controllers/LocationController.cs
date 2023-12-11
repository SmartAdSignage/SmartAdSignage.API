using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.DTOs.Location.Requests;
using SmartAdSignage.Core.DTOs.Location.Responses;
using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly Serilog.ILogger _logger;

        public LocationController(IMapper mapper, ILocationService locationService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _locationService = locationService;
            _logger = logger;
        }

        [HttpGet]
        [Route("locations")]
        public async Task<IActionResult> GetLocations([FromQuery] GetRequest getRequest)
        {
            var result = await _locationService.GetAllLocationsAsync(getRequest.PageInfo);
            if (!result.Any() || result == null)
            {
                _logger.Error("No locations found");
                return NotFound("No locations found");
            }
            var locations = _mapper.Map<List<LocationResponse>>(result);
            return Ok(locations);
        }

        [HttpGet]
        [Route("location/{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var result = await _locationService.GetLocationByIdAsync(id);
            if (result == null)
            {
                _logger.Error($"Location with id:{id} not found");
                return NotFound($"Location with id:{id} not found");
            }
            var location = _mapper.Map<LocationResponse>(result);
            return Ok(location);
        }

        [HttpDelete]
        [Route("location/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var result = await _locationService.DeleteLocationByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"Location with id:{id} not found");
                return NotFound($"Location with id:{id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("location")]
        public async Task<IActionResult> CreateLocation(LocationRequest locationRequest)
        {
            var location = _mapper.Map<Location>(locationRequest);
            var result = await _locationService.CreateLocationAsync(location);
            if (result == null)
            {
                _logger.Error("Coulnd't create location");
                return BadRequest("Coulnd't create location");
            }
            var locationResponse = _mapper.Map<LocationResponse>(result);
            return Ok(locationResponse);
        }

        [HttpPut]
        [Route("location/{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationRequest locationRequest)
        {
            var location = _mapper.Map<Location>(locationRequest);
            var result = await _locationService.UpdateLocationAsync(id, location);
            if (result == null)
            {
                _logger.Error($"Location with id: {id} not found");
                return NotFound($"Location with id: {id} not found");
            }
            var locationResponse = _mapper.Map<LocationResponse>(result);
            return Ok(locationResponse);
        }
    }
}
