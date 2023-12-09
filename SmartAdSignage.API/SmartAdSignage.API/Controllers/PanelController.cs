using AutoMapper;
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
        [Route("panels/{id}")]
        public async Task<IActionResult> GetPanelsByUserId(string id)
        {
            var result = await _panelService.GetAllPanelsByUserIdAsync(id);
            if (!result.Any() || result == null)
            {
                _logger.Error($"No panels found for user with id:{id}");
                return NotFound($"No panels found for user with id:{id}");
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

        [HttpPost("change")]
        public async Task<IActionResult> ChangeBrightness()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);
                // Replace the base address with your actual address
                httpClient.BaseAddress = new Uri("http://localhost:9080/");

                // Create the request URL
                string requestUrl = $"change/1";

                try
                {
                    // Send a POST request to the specified URL
                    HttpResponseMessage response = await httpClient.PostAsync(requestUrl, null);
                    string content = await response.Content.ReadAsStringAsync();
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    double luxValue;
                    bool toggleValue = double.TryParse(content, NumberStyles.Any, culture, out luxValue);
                    // double luxValue = Convert.ToDouble(content);
                    // Check if the request was successful (status code 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok("Toggle request successful");
                    }
                    else
                    {
                        // If the request was not successful, return an error message
                        return BadRequest($"Toggle request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the request
                    return StatusCode(500, $"An error occurred while sending the toggle request: {ex.Message}");
                }
            }
        }
    }
}
