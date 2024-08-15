using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.User;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRole([FromQuery] RemoveRoleDto request)
        {
            await _userService.RemoveRole(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser([FromQuery] AddRoleToUserDto request)
        {
            await _userService.AddRoleToUser(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersRoles()
        {
            return Ok(await _userService.GetUsersRoles());
        }
    }
}
