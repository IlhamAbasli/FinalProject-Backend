using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Wallet;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFunds([FromQuery] WalletCreateDto request)
        {
            await _walletService.AddFunds(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance([FromQuery] string userId)
        {
            return Ok(await _walletService.GetUserBalance(userId));
        }
    }
}
