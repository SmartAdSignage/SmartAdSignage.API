using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelController(IMapper mapper, IPanelService panelService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPanelService _panelService = panelService;

        [HttpGet]
        [Route("panels")]
        public async Task<IActionResult> GetPanels()
        {
            var result = await _panelService.GetAllPanelsAsync();
            if (result.Count() == 0)
                return NotFound();
            var panels = _mapper.Map<IEnumerable<PanelResponse>>(result);
            return Ok(panels);
        }
    }
}
