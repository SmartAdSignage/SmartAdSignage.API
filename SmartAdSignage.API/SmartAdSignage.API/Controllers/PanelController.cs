using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.DTOs.Panel.Requests;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;
using System.Globalization;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PanelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPanelService _panelService;
        private readonly Serilog.ILogger _logger;

        public PanelController(IMapper mapper, IPanelService panelService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _panelService = panelService;
            _logger = logger;
        }

        [HttpGet]
        [Route("panels")]
        public async Task<IActionResult> GetPanels([FromQuery] GetRequest getRequest)
        {
            var result = await _panelService.GetAllPanelsAsync(getRequest.PageInfo);
            if (!result.Any() || result == null) 
            {
                _logger.Error("No panels found");
                return NotFound("No panels found");
            }
            var panels = _mapper.Map<IEnumerable<PanelResponse>>(result);
            return Ok(panels);
        }

        [HttpGet]
        [Route("panel/{id}")]
        public async Task<IActionResult> GetPanel(int id)
        {
            var result = await _panelService.GetPanelByIdAsync(id);
            if (result == null)
            {
                _logger.Error($"Panel with id:{id} not found");
                return NotFound($"Panel with id:{id} not found");
            }
            var panel = _mapper.Map<PanelResponse>(result);
            return Ok(panel);
        }

        [HttpGet]
        [Route("panels/{userId}")]
        public async Task<IActionResult> GetPanelsByUserId(string userId)
        {
            var result = await _panelService.GetAllPanelsByUserIdAsync(userId);
            if (!result.Any() || result == null)
            {
                _logger.Error($"No panels found for user with id:{userId}");
                return NotFound($"No panels found for user with id:{userId}");
            }
            var panels = _mapper.Map<IEnumerable<PanelResponse>>(result);
            return Ok(panels);
        }

        [HttpDelete]
        [Route("panel/{id}")]
        public async Task<IActionResult> DeletePanel(int id)
        {
            var result = await _panelService.DeletePanelByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"Panel with id:{id} not found");
                return NotFound($"Panel with id:{id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("panel")]
        public async Task<IActionResult> CreatePanel(PanelRequest panelRequest)
        {
            var panel = _mapper.Map<Panel>(panelRequest);
            var result = await _panelService.CreatePanelAsync(panel);
            if (result == null)
            {
                _logger.Error("Coulnd't create panel");
                return BadRequest("Coulnd't create panel");
            }
            var panelResponse = _mapper.Map<PanelResponse>(result);
            return Ok(panelResponse);
        }

        [HttpPut]
        [Route("panel/{id}")]
        public async Task<IActionResult> UpdatePanel(int id, PanelRequest panelRequest)
        {
            var panel = _mapper.Map<Panel>(panelRequest);
            var result = await _panelService.UpdatePanelAsync(id, panel);
            if (result == null)
            {
                _logger.Error($"Panel with id:{id} not found");
                return NotFound($"Panel with id: {id} not found");
            }
            var panelResponse = _mapper.Map<PanelResponse>(result);
            return Ok(panelResponse);
        }

        [HttpPut("change-brightness/{id}")]
        public async Task<IActionResult> ChangeBrightness(int id)
        {
            var result = await _panelService.ChangePanelBrightness(id);
            if (result == null)
            {
                _logger.Error($"Panel with id:{id} not found or it doesn't have light meter to execute the task");
                return NotFound($"Panel with id:{id} not found or it doesn't have light meter to execute the task");
            }
            var panelResponse = _mapper.Map<PanelResponse>(result);
            return Ok(panelResponse);
        }
    }
}
