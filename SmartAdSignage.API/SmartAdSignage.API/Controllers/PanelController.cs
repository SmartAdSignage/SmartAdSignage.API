using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.DTOs.Panel.Requests;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPanelService _panelService;

        public PanelController(IMapper mapper, IPanelService panelService)
        {
            _mapper = mapper;
            _panelService = panelService;
        }

        [HttpGet]
        [Route("panels")]
        public async Task<IActionResult> GetPanels([FromQuery] GetRequest getRequest)
        {
            var result = await _panelService.GetAllPanelsAsync(getRequest.PageInfo);
            if (result.Count() == 0)
                return NotFound();
            var panels = _mapper.Map<IEnumerable<PanelResponse>>(result);
            return Ok(panels);
        }

        [HttpGet]
        [Route("panel/{id}")]
        public async Task<IActionResult> GetPanel(int id)
        {
            var result = await _panelService.GetPanelByIdAsync(id);
            if (result == null)
                return NotFound();
            var panel = _mapper.Map<PanelResponse>(result);
            return Ok(panel);
        }

        [HttpDelete]
        [Route("panel/{id}")]
        public async Task<IActionResult> DeletePanel(int id)
        {
            var result = await _panelService.DeletePanelByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("panel")]
        public async Task<IActionResult> CreatePanel(PanelRequest panelRequest)
        {
            var panel = _mapper.Map<Panel>(panelRequest);
            var result = await _panelService.CreatePanelAsync(panel);
            if (result == null)
                return NotFound();
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
                return NotFound();
            var panelResponse = _mapper.Map<PanelResponse>(result);
            return Ok(panelResponse);
        }
    }
}
