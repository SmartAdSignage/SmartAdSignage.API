using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class QueueController(IMapper mapper, IQueueService queueService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IQueueService _queueService = queueService;

        [HttpGet]
        [Route("queues")]
        public async Task<IActionResult> GetQueues()
        {
            var result = await _queueService.GetAllQueuesAsync();
            if (result.Count() == 0)
                return NotFound();
            var panels = _mapper.Map<IEnumerable<QueueResponse>>(result);
            return Ok(panels);
        }

        [HttpGet]
        [Route("queue/{id}")]
        public async Task<IActionResult> GetQueue(int id)
        {
            var result = await _queueService.GetQueueByIdAsync(id);
            if (result == null)
                return NotFound();
            var panel = _mapper.Map<QueueResponse>(result);
            return Ok(panel);
        }

        [HttpDelete]
        [Route("queue/{id}")]
        public async Task<IActionResult> DeleteQueue(int id)
        {
            var result = await _queueService.DeleteQueueByIdAsync(id);
            if (result is false)
                return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("queue")]
        public async Task<IActionResult> CreateQueue(QueueRequest queueRequest)
        {
            var queue = _mapper.Map<Queue>(queueRequest);
            var result = await _queueService.CreateQueueAsync(queue);
            if (result == null)
                return NotFound();
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
                return NotFound();
            var queueResponse = _mapper.Map<QueueResponse>(result);
            return Ok(queueResponse);
        }
    }
}
