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
        /*Task<Queue> CreateQueueAsync(Queue queue);
        Task<Queue> UpdateQueueAsync(Queue queue);
        Task<Queue> DeleteQueueAsync(Queue queue);
        Task<Queue> GetByIdAsync(int id);*/
        Task<IEnumerable<Queue>> GetAllQueuesAsync();
    }
}
