using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.IoTDevice.Responses;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTDeviceController(IMapper mapper, IIoTDeviceService ioTDeviceService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IIoTDeviceService _ioTDeviceService = ioTDeviceService;
        
        [HttpGet]
        [Route("IoTDevices")]
        public async Task<IActionResult> GetIoTDevices()
        {
            var result = await _ioTDeviceService.GetAllIoTDevicesAsync();
            if (result.Count() == 0)
                return NotFound();
            var IoTDevices = _mapper.Map<IEnumerable<IoTDeviceResponse>>(result);
            return Ok(IoTDevices);
        }

    }
}
