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
        private readonly IUnitOfWork _unitOfWork;
        public QueueService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Queue> CreateQueueAsync(Queue queue)
        {
            var result = await _unitOfWork.Queues.AddAsync(queue);
            await _unitOfWork.Queues.Commit();
            return result;
        }

        public async Task<bool> DeleteQueueByIdAsync(int id)
        {
            var queue = await _unitOfWork.Queues.GetByIdAsync(id);
            var result = _unitOfWork.Queues.DeleteAsync(queue);
            await _unitOfWork.Queues.Commit();
            return result;
        }

        public async Task<IEnumerable<Queue>> GetAllQueuesAsync()
        {
            return await _unitOfWork.Queues.GetAllAsync();
        }

        public async Task<Queue> GetQueueByIdAsync(int id)
        {
            return await _unitOfWork.Queues.GetByIdAsync(id);
        }

        public async Task<Queue> UpdateQueueAsync(int id, Queue queue)
        {
            var existingQueue = await _unitOfWork.Queues.GetByIdAsync(id);
            existingQueue.DisplayOrder = queue.DisplayOrder;
            existingQueue.AdvertisementId = queue.AdvertisementId;
            existingQueue.PanelId = queue.PanelId;
            existingQueue.DateUpdated = DateTime.Now;
            var result = _unitOfWork.Queues.UpdateAsync(existingQueue);
            await _unitOfWork.Queues.Commit();
            return result;
        }
    }
}
