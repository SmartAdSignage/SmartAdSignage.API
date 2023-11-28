using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SmartAdSignage.Core.DTOs.Requests;
using SmartAdSignage.Core.DTOs.Responses;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using SmartAdSignage.Core.Models;
using AutoMapper;
using SmartAdSignage.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(SignInManager<User> signInManager, IMapper mapper, IUsersService usersService) : ControllerBase
    {
        private readonly SignInManager<User> signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUsersService _usersService = usersService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!await _usersService.ValidateUserAsync(loginRequest))
                return Unauthorized();
            string[]? tokens = await _usersService.GenerateTokensAsync();
            return Ok(new AuthenticatedResponse { TokenType = "Bearer", Token = tokens[0], Expiration = DateTime.Now.AddMinutes(1), RefreshToken = tokens[1]});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var userResult = await _usersService.RegisterUserAsync(registerRequest);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (refreshRequest is null)
                return BadRequest("Invalid client request");
            var newTokens = await _usersService.RefreshTokensAsync(refreshRequest);
            if (newTokens is null)
                return Unauthorized();
            return Ok(new AuthenticatedResponse { TokenType = "Bearer", Token = newTokens[0], Expiration = DateTime.Now.AddMinutes(1), RefreshToken = newTokens[1] });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeRequest revokeRequest)
        {
            if (revokeRequest is null)
                return BadRequest("Invalid client request");

            var result = await _usersService.RevokeToken(revokeRequest.UserName);
            if (result is null)
                return BadRequest("Invalid user name");

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result);
        }
    }
}
