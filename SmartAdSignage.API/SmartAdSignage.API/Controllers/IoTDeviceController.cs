using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.DTOs.IoTDevice.Requests;
using SmartAdSignage.Core.DTOs.IoTDevice.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTDeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIoTDeviceService _ioTDeviceService;

        public IoTDeviceController(IMapper mapper, IIoTDeviceService ioTDeviceService)
        {
            _mapper = mapper;
            _ioTDeviceService = ioTDeviceService;
        }
        
        [HttpGet]
        [Route("IoTDevices")]
        public async Task<IActionResult> GetIoTDevices([FromQuery] GetRequest getRequest)
        {
            var result = await _ioTDeviceService.GetAllIoTDevicesAsync(getRequest.PageInfo);
            if (result.Count() == 0)
                return NotFound();
            var IoTDevices = _mapper.Map<IEnumerable<IoTDeviceResponse>>(result);
            return Ok(IoTDevices);
        }

        [HttpGet]
        [Route("IoTDevice/{id}")]
        public async Task<IActionResult> GetIoTDevice(int id)
        {
            var result = await _ioTDeviceService.GetIoTDeviceByIdAsync(id);
            if (result == null)
                return NotFound();
            var IoTDevice = _mapper.Map<IoTDeviceResponse>(result);
            return Ok(IoTDevice);
        }

        [HttpDelete]
        [Route("IoTDevice/{id}")]
        public async Task<IActionResult> DeleteIoTDevice(int id)
        {
            var result = await _ioTDeviceService.DeleteIoTDeviceByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("IoTDevice")]
        public async Task<IActionResult> CreateIoTDevice(IoTDeviceRequest ioTDeviceRequest)
        {
            var ioTDevice = _mapper.Map<IoTDevice>(ioTDeviceRequest);
            var result = await _ioTDeviceService.CreateIoTDeviceAsync(ioTDevice);
            if (result == null)
                return NotFound();
            var ioTDeviceResponse = _mapper.Map<IoTDeviceResponse>(result);
            return Ok(ioTDeviceResponse);
        }

        [HttpPut]
        [Route("IoTDevice/{id}")]
        public async Task<IActionResult> UpdateIoTDevice(int id, IoTDeviceRequest ioTDeviceRequest)
        {
            var ioTDevice = _mapper.Map<IoTDevice>(ioTDeviceRequest);
            var result = await _ioTDeviceService.UpdateIoTDeviceAsync(id, ioTDevice);
            if (result == null)
                return NotFound();
            var ioTDeviceResponse = _mapper.Map<IoTDeviceResponse>(result);
            return Ok(ioTDeviceResponse);
        }
    }
}
