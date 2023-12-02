using Microsoft.AspNetCore.Identity;
using SmartAdSignage.Core.DTOs.User.Requests;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);

        Task<IdentityResult> RegisterUserAsync(User user, string password);

        Task<IdentityResult> RegisterAdminAsync(User user, string password);

        Task<IdentityResult> RevokeToken(string username);

        Task<string[]?> GenerateTokensAsync();

        Task<string[]?> RefreshTokensAsync(string accessToken, string refreshToken);

        Task<IEnumerable<User?>> GetUsersAsync();

        Task<User> GetUserByNameAsync(string userName);

        Task<IdentityResult> DeleteUserByNameAsync(string userName);

        Task<IdentityResult> UpdateUserAsync(string userName, User user);

        string GetConfiguration(string setting);
    }
}
