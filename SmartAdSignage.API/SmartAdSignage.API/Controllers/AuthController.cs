using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using SmartAdSignage.Core.Models;
using AutoMapper;
using SmartAdSignage.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SmartAdSignage.Core.DTOs.User.Responses;
using SmartAdSignage.Core.DTOs.User.Requests;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _usersService;
        private readonly Serilog.ILogger _logger;

        public AuthController(IMapper mapper, IUserService usersService, Serilog.ILogger logger)
        {
            _mapper = mapper;
            _usersService = usersService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!await _usersService.ValidateUserAsync(loginRequest.UserName, loginRequest.Password))
            {
                _logger.Error($"Login failed for user {loginRequest.UserName}");
                return Unauthorized($"Login failed for user {loginRequest.UserName}");
            }
            string[]? tokens = await _usersService.GenerateTokensAsync();
            return Ok(new AuthenticatedResponse { TokenType = "Bearer", 
                Token = tokens[0], 
                Expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_usersService.GetConfiguration("accessTokenExpiresInMinutes"))),
                RefreshToken = tokens[1]});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Registration failed for user {registerRequest.Email}");
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(registerRequest);
            string password = registerRequest.Password;
            var userResult = await _usersService.RegisterUserAsync(user, password);
            var createdUser = _mapper.Map<RegisteredUserResponse>(user);
            createdUser.Role = "User";
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : Created("", createdUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Registration failed for user {registerRequest.Email}");
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(registerRequest);
            string password = registerRequest.Password;
            var userResult = await _usersService.RegisterUserAsync(user, password);
            var createdUser = _mapper.Map<RegisteredUserResponse>(user);
            createdUser.Role = "Admin";
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : Created("", createdUser);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (refreshRequest is null)
            {
                _logger.Error($"Invalid client request");
                return BadRequest("Invalid client request");
            }
            var newTokens = await _usersService.RefreshTokensAsync(refreshRequest.Token, refreshRequest.RefreshToken);
            if (newTokens is null)
            {
                _logger.Error($"Expired or invalid refresh token");
                return Unauthorized();
            }
            return Ok(new AuthenticatedResponse { TokenType = "Bearer", 
                Token = newTokens[0], 
                Expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_usersService.GetConfiguration("accessTokenExpiresInMinutes"))), 
                RefreshToken = newTokens[1] });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeRequest revokeRequest)
        {
            if (revokeRequest is null)
            {
                _logger.Error($"Invalid client request");
                return BadRequest("Invalid client request");
            }

            var result = await _usersService.RevokeToken(revokeRequest.UserName);
            if (result is null)
            {
                _logger.Error($"Invalid username:{revokeRequest.UserName}");
                return BadRequest($"Invalid username:{revokeRequest.UserName}");
            }

            if (result.Succeeded)
                return NoContent();

            _logger.Error($"Couldn't revoke token for user:{revokeRequest.UserName}");
            return BadRequest(result);
        }
    }
}
