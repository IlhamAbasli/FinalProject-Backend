using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Basket;
using Service.DTOs.Wishlist;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket([FromQuery] AddBasketDto request)
        {
            await _basketService.AddBasket(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserBasket([FromQuery] string userId)
        {
            return Ok(await _basketService.GetUserBasket(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBasketIds([FromQuery] string userId)
        {
            return Ok(await _basketService.GetUserBasketIds(userId));
        }



        [HttpDelete]
        public async Task<IActionResult> RemoveFromBasket([FromQuery] RemoveWishlistDto request)
        {
            var existData = await _basketService.GetUserBasketProductById(request.ProductId, request.UserId);
            await _basketService.RemoveFromBasket(existData);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> BuyProducts(BuyProductDto request)
        {
            await _basketService.BuyProducts(request);
            return Ok();
        }
    }
}
