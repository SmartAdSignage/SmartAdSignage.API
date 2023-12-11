using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Common;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.DTOs.Queue.Requests;
using SmartAdSignage.Core.DTOs.Queue.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Implementations;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QueueController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;
        private readonly Serilog.ILogger _logger;

        public QueueController(IMapper mapper, IQueueService queueService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _queueService = queueService;
            _logger = logger;
        }

        [HttpGet]
        [Route("queues")]
        public async Task<IActionResult> GetQueues([FromQuery] GetRequest getRequest)
        {
            var result = await _queueService.GetAllQueuesAsync(getRequest.PageInfo);
            if (!result.Any() || result == null) 
            {
                _logger.Error("No queues found");
                return NotFound("No queues found");
            }
            var panels = _mapper.Map<IEnumerable<QueueResponse>>(result);
            return Ok(panels);
        }

        [HttpGet]
        [Route("queue/{id}")]
        public async Task<IActionResult> GetQueue(int id)
        {
            var result = await _queueService.GetQueueByIdAsync(id);
            if (result == null)
            {
                _logger.Error($"Queue with id:{id} not found");
                return NotFound($"Queue with id:{id} not found");
            }
            var panel = _mapper.Map<QueueResponse>(result);
            return Ok(panel);
        }

        [HttpGet]
        [Route("panel-queues/{panelId}")]
        public async Task<IActionResult> GetQueuesByPanelId(int panelId)
        {
            var result = await _queueService.GetAllQueuesByPanelIdAsync(panelId);
            if (!result.Any() || result == null)
            {
                _logger.Error($"No queues found for panel with id:{panelId}");
                return NotFound($"No queues found for panel with id:{panelId}");
            }
            var panels = _mapper.Map<IEnumerable<QueueResponse>>(result);
            return Ok(panels);
        }

        [HttpGet]
        [Route("advertisement-queues/{advertisementId}")]
        public async Task<IActionResult> GetQueuesByAdvertisementId(int advertisementId)
        {
            var result = await _queueService.GetAllQueuesByAdvertisementIdAsync(advertisementId);
            if (!result.Any() || result == null) 
            {
                _logger.Error($"No queues found for advertisement with id:{advertisementId}");
                return NotFound($"No queues found for advertisement with id:{advertisementId}");
            }
            var panels = _mapper.Map<IEnumerable<QueueResponse>>(result);
            return Ok(panels);
        }

        [HttpDelete]
        [Route("queue/{id}")]
        public async Task<IActionResult> DeleteQueue(int id)
        {
            var result = await _queueService.DeleteQueueByIdAsync(id);
            if (result is false)
            {
                _logger.Error($"Queue with id:{id} not found");
                return NotFound($"Queue with id:{id} not found");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("queue")]
        public async Task<IActionResult> CreateQueue(QueueRequest queueRequest)
        {
            var queue = _mapper.Map<Queue>(queueRequest);
            var result = await _queueService.CreateQueueAsync(queue);
            if (result == null)
            {
                _logger.Error("Couldn't create queue");
                return BadRequest("Couldn't create queue");
            }
            var queueResponse = _mapper.Map<QueueResponse>(result);
            return Ok(queueResponse);
        }

        [HttpPut]
        [Route("queue/{id}")]
        public async Task<IActionResult> UpdateQueue(int id, QueueRequest queueRequest)
        {
            var queue = _mapper.Map<Queue>(queueRequest);
            var result = await _queueService.UpdateQueueAsync(id, queue);
            if (result == null)
            {
                _logger.Error($"Queue with id:{id} not found");
                return NotFound($"Queue with id:{id} not found");
            }
            var queueResponse = _mapper.Map<QueueResponse>(result);
            return Ok(queueResponse);
        }
    }
}
