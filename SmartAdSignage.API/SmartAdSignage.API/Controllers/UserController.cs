using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAdSignage.Core.DTOs.User.Requests;
using SmartAdSignage.Core.DTOs.User.Responses;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Services.Services.Implementations;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMapper mapper, IUserService usersService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _usersService = usersService;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _usersService.GetUsersAsync();
            if (result is null)
                return NotFound();
            var users = _mapper.Map<IEnumerable<UserResponse>>(result);
            return Ok(users);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserByName([FromRoute] string userName)
        {
            var result = await _usersService.GetUserByNameAsync(userName);
            if (result is null)
                return NotFound();
            var user = _mapper.Map<UserResponse>(result);
            return Ok(user);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userName)
        {
            var result = await _usersService.DeleteUserByNameAsync(userName);
            if (result is null)
                return NotFound();
            if (!result.Succeeded)
                return BadRequest(result);
            return NoContent();
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string username, [FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = _mapper.Map<User>(updateUserRequest);
            var result = await _usersService.UpdateUserAsync(username, user);
            if (result is null)
                return NotFound();
            if (!result.Succeeded)
                return BadRequest(result);
            return Ok(_mapper.Map<UserResponse>(_usersService.GetUserByNameAsync(updateUserRequest.Email).Result));
        }
    }
}
