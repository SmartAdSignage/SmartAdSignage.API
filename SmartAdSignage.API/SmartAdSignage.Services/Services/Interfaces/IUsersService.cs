using Microsoft.AspNetCore.Identity;
using SmartAdSignage.Core.DTOs.Requests;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IUsersService
    {
        Task<bool> ValidateUserAsync(LoginRequest loginRequest);

        Task<IdentityResult> RegisterUserAsync(RegisterRequest registerRequest);

        Task<IdentityResult> RevokeToken(string username);

        Task<string[]?> GenerateTokensAsync();

        Task<string[]?> RefreshTokensAsync(RefreshRequest refreshRequest);
    }
}
