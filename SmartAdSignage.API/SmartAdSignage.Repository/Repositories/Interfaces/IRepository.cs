using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
