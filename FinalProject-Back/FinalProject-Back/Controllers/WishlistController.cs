using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Wishlist;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlishtService _wishlistService;
        public WishlistController(IWishlishtService wishlishtService)
        {
            _wishlistService = wishlishtService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWishlist([FromQuery] AddWishlistDto request)
        {
            await _wishlistService.AddWishlist(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserWishlist([FromQuery] string userId)
        {
            return Ok(await _wishlistService.GetUserWishlist(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWishlistIds([FromQuery] string userId)
        {
            return Ok(await _wishlistService.GetUserWishlistIds(userId));
        }

        [HttpGet]
        public async Task<IActionResult> SortBy([FromQuery] string userId, [FromQuery] string sortType)
        {
            var wishlistDatas = await _wishlistService.GetUserWishlist(userId);
            switch (sortType)
            {
                case "Recently Added":
                    wishlistDatas = wishlistDatas.OrderByDescending(x=>x.Id).ToList();
                    break;
                case "Alphabetical":
                    wishlistDatas = wishlistDatas.OrderBy(x => x.Product.ProductName).ToList();
                    break;
                case "Price: High to Low":
                    wishlistDatas = wishlistDatas.OrderByDescending(x=>x.Product.ProductPrice).ToList();
                    break;
                case "Price: Low to High":
                    wishlistDatas = wishlistDatas.OrderBy(x => x.Product.ProductPrice).ToList();
                    break;
                default:
                    return Ok(wishlistDatas);
            }

            return Ok(wishlistDatas);   
        }



        [HttpDelete]
        public async Task<IActionResult> RemoveFromWishlist([FromQuery] RemoveWishlistDto request)
        {
            var existData = await _wishlistService.GetUserWishlistProductById(request.ProductId, request.UserId);
            await _wishlistService.RemoveFromWishlist(existData);
            return Ok();
        }
    }
}
