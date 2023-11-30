using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class QueueService : IQueueService
    {
        private readonly IGenericRepository<Queue> _queueRepository;

        public QueueService(IGenericRepository<Queue> queueRepository)
        {
            this._queueRepository = queueRepository;
        }

        public Task<Queue> CreateQueueAsync(Queue queue)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Queue>> GetAllQueuesAsync()
        {
            return await _queueRepository.GetAllAsync();
        }
    }
}
