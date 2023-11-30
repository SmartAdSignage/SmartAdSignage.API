using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Location.Responses;
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
            var locations = _mapper.Map<IEnumerable<LocationResponse>>(result);
            return Ok(locations);
        }
    }
}
