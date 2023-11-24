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

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly SignInManager<User> signInManager;
        public AuthController(IRepository repository, SignInManager<User> signInManager)
        {
            this.repository = repository;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequest user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            var users = await repository.GetAllAsync();
            foreach (var username in users)
            {
                if (username.UserName == user.UserName)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(username, user.Password, true);
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
                }
            }

            /*if (user.UserName == "vasya" && user.Password == "Pass123$")
            {
                
            }*/

            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest user)
        {
            return Ok();
        }
    }
}
