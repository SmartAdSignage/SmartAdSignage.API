using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController(IMapper mapper, IAdvertisementService advertisementService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAdvertisementService _advertisementService = advertisementService;

        [HttpGet]
        [Route("advertisements")]
        public async Task<IActionResult> GetAdvertisements()
        {
            var result = await _advertisementService.GetAllAdvertisements();
            if (result.Count() == 0)
                return NotFound();
            var advertisements = _mapper.Map<IEnumerable<AdvertisementResponse>>(result);
            return Ok(advertisements);
        }
    }
}
