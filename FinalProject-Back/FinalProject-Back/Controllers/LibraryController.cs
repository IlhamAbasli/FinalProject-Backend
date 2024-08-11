using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Library;
using Service.DTOs.Product;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLibraryByRedeem([FromQuery]AddLibraryDto request)
        {
            await _libraryService.AddLibraryByRedeem(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] string userId, [FromQuery] string sortType,[FromQuery] int page = 1)
        {
            var paginatedDatas = await _libraryService.GetAllPaginatedProducts(page,userId,sortType);
            var productsCount = await _libraryService.GetCount(userId);
            var pageCount = _libraryService.GetLibraryPageCount(productsCount, 8);

            var model = new LibraryPageDto { LibraryProducts = paginatedDatas, PageCount = pageCount };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLibraryIds([FromQuery] string userId)
        {
            return Ok(await _libraryService.GetUserLibraryIds(userId));
        }
    }
}
