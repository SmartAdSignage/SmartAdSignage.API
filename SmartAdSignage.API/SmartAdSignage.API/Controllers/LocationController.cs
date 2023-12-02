using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Location.Requests;
using SmartAdSignage.Core.DTOs.Location.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController(IMapper mapper, ILocationService locationService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILocationService _locationService = locationService;
        
        [HttpGet]
        [Route("locations")]
        public async Task<IActionResult> GetLocations()
        {
            var result = await _locationService.GetAllLocationsAsync();
            if (result.Count() == 0)
                return NotFound();
            var locations = _mapper.Map<List<LocationResponse>>(result);
            return Ok(locations);
        }

        [HttpGet]
        [Route("location/{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var result = await _locationService.GetLocationByIdAsync(id);
            if (result == null)
                return NotFound();
            var location = _mapper.Map<LocationResponse>(result);
            return Ok(location);
        }

        [HttpDelete]
        [Route("location/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var result = await _locationService.DeleteLocationByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("location")]
        public async Task<IActionResult> CreateLocation(LocationRequest locationRequest)
        {
            var location = _mapper.Map<Location>(locationRequest);
            var result = await _locationService.CreateLocationAsync(location);
            if (result == null)
                return NotFound();
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
                return NotFound();
            var locationResponse = _mapper.Map<LocationResponse>(result);
            return Ok(locationResponse);
        }
    }
}
