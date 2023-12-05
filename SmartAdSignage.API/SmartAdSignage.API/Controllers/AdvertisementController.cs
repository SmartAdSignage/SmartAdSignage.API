using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public AdvertisementController(IMapper mapper, IAdvertisementService advertisementService)
        {
            _mapper = mapper;
            _advertisementService = advertisementService;
        }

        [HttpGet]
        [Route("advertisements")]
        public async Task<IActionResult> GetAdvertisements([FromQuery] GetRequest getRequest)
        {
            var result = await _advertisementService.GetAllAdvertisements(getRequest.PageInfo);
            if (result.Count() == 0)
                return NotFound();
            var advertisements = _mapper.Map<IEnumerable<AdvertisementResponse>>(result);
            return Ok(advertisements);
        }

        [HttpGet]
        [Route("advertisement/{id}")]
        public async Task<IActionResult> GetAdvertisement(int id)
        {
            var result = await _advertisementService.GetAdvertisementByIdAsync(id);
            if (result == null)
                return NotFound();
            var advertisement = _mapper.Map<AdvertisementResponse>(result);
            return Ok(advertisement);
        }

        [HttpDelete]
        [Route("advertisement/{id}")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            var result = await _advertisementService.DeleteAdvertisementByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("advertisement")]
        public async Task<IActionResult> CreateAdvertisement(AdvertisementRequest advertisementRequest)
        {
            var advertisement = _mapper.Map<Advertisement>(advertisementRequest);
            var result = await _advertisementService.CreateAdvertisementAsync(advertisement);
            if (result == null)
                return BadRequest();
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
                return NotFound();
            var advertisementResponse = _mapper.Map<AdvertisementResponse>(result);
            return Ok(advertisementResponse);
        }
    }
}
