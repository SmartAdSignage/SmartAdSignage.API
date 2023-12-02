using Microsoft.AspNetCore.Identity;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByUsernameAsync(string username);

        Task<bool> SaveRefreshToken(string username, string refreshToken);

        Task<IdentityResult> UpdateUser(User user);

        Task<IdentityResult> CreateUserAsync(User user, string password);

        Task<IdentityResult> AddRoleToUserAsync(User user, string role);

        Task<IList<string>> GetRolesForUserAsync(User user);

        Task<bool> CheckPasswordForUserAsync(User user, string password);

        Task<IdentityResult> DeleteUserAsync(User user);
    }
}
