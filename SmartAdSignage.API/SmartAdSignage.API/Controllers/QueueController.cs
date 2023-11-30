using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.DTOs.Queue.Responses;
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
    }
}
