using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByUsernameAsync(string username);

        Task<bool> SaveRefreshToken(string username, string refreshToken);
    }
}
