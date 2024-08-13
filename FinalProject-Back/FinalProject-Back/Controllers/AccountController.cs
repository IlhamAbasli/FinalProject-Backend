using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using Org.BouncyCastle.Bcpg;
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
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto request)
        {
            await _accountService.ConfirmEmail(request.userId, request.token);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] LoginDto request)
        {
            var response = await _accountService.SignIn(request);
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromQuery] string userId, [FromForm] UserUpdateDto request)
        {
            await _accountService.UpdateUser(userId,request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ForgetPassword([FromQuery] ForgetPasswordDto request)
        {
            return Ok(await _accountService.ForgetPassword(request));
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto request)
        {
            await _accountService.ResetPassword(request);
            return Ok();
        }
    }
}
