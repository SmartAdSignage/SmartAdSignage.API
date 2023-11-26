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

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository repository;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        public AuthController(IUsersRepository repository, SignInManager<User> signInManager, IMapper mapper, IUsersService usersService)
        {
            this.repository = repository;
            this.signInManager = signInManager;
            this._mapper = mapper;
            this._usersService = usersService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequest loginRequest)
        {
            return !await _usersService.ValidateUserAsync(loginRequest)
            ? Unauthorized()
            : Ok(new AuthenticatedResponse { Token = await _usersService.CreateTokenAsync() });
            /*if (loginRequest is null)
            {
                return BadRequest("Invalid client request");
            }

            if (await _usersService.ValidateUser(loginRequest) == false)
            {
                return Unauthorized();
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
            if (result.Succeeded)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString });
            }

            return Unauthorized();*/
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var userResult = await _usersService.RegisterUserAsync(registerRequest);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }
    }
}
