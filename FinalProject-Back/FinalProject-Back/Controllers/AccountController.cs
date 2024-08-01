using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Account;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            await _accountService.CreateRoles();
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] RegisterDto request)
        {
            return Ok(await _accountService.SignUp(request));
        }


        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] LoginDto request)
        {
            var response = await _accountService.SignIn(request);
            return Ok(response);
        }
    }
}
