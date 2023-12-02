using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IQueueService
    {
        Task<Queue> CreateQueueAsync(Queue queue);
        Task<Queue> UpdateQueueAsync(int id, Queue queue);
        Task<bool> DeleteQueueByIdAsync(int id);
        Task<Queue> GetQueueByIdAsync(int id);
        Task<IEnumerable<Queue>> GetAllQueuesAsync();
    }
}
