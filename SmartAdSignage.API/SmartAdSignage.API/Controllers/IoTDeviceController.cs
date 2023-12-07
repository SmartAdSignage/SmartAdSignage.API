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
        private readonly Serilog.ILogger _logger;

        public IoTDeviceController(IMapper mapper, IIoTDeviceService ioTDeviceService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _ioTDeviceService = ioTDeviceService;
            _logger = logger;
        }

        [HttpGet]
        [Route("IoTDevices")]
        public async Task<IActionResult> GetIoTDevices([FromQuery] GetRequest getRequest)
        {
            var result = await _ioTDeviceService.GetAllIoTDevicesAsync(getRequest.PageInfo);
            if (!result.Any() || result == null) 
            {
                _logger.Error("No IoT devices found");
                return NotFound("No IoT devices found");
            }
            var IoTDevices = _mapper.Map<IEnumerable<IoTDeviceResponse>>(result);
            return Ok(IoTDevices);
        }

        [HttpGet]
        [Route("IoTDevice/{id}")]
        public async Task<IActionResult> GetIoTDevice(int id)
        {
            var result = await _ioTDeviceService.GetIoTDeviceByIdAsync(id);
            if (result == null)
            {
                _logger.Error($"IoT device with id:{id} not found");
                return NotFound($"IoT device with id:{id} not found");
            }
            var IoTDevice = _mapper.Map<IoTDeviceResponse>(result);
            return Ok(IoTDevice);
        }

        [HttpGet]
        [Route("IoTDevices/{id}")]
        public async Task<IActionResult> GetIoTDevicesByPanelId(int id)
        {
            var result = await _ioTDeviceService.GetAllIoTDevicesByPanelIdAsync(id);
            if (!result.Any() || result == null) 
            {
                _logger.Error($"No IoT devices found for panel with id:{id}");
                return NotFound($"No IoT devices found for panel with id:{id}");
            }
            var IoTDevices = _mapper.Map<IEnumerable<IoTDeviceResponse>>(result);
            return Ok(IoTDevices);
        }

        [HttpDelete]
        [Route("IoTDevice/{id}")]
        public async Task<IActionResult> DeleteIoTDevice(int id)
        {
            var result = await _ioTDeviceService.DeleteIoTDeviceByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"IoT device with id:{id} not found");
                return NotFound($"IoT device with id:{id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("IoTDevice")]
        public async Task<IActionResult> CreateIoTDevice(IoTDeviceRequest ioTDeviceRequest)
        {
            var ioTDevice = _mapper.Map<IoTDevice>(ioTDeviceRequest);
            var result = await _ioTDeviceService.CreateIoTDeviceAsync(ioTDevice);
            if (result == null)
            {
                _logger.Error($"Couldn't create IoT device");
                return BadRequest($"Couldn't create IoT device");
            }
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
            {
                _logger.Error($"IoT device with id: {id} not found");
                return NotFound($"IoT device with id: {id} not found");
            }
            var ioTDeviceResponse = _mapper.Map<IoTDeviceResponse>(result);
            return Ok(ioTDeviceResponse);
        }
    }
}
