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
        public async Task<IActionResult> GetAllPaginated([FromQuery] string userId, [FromQuery] string sortType, [FromQuery] string searchText, [FromQuery] string[] genreFilters, [FromQuery] string[] typeFilters, [FromQuery] int page = 1)
        {
            var paginatedDatas = await _libraryService.GetAllPaginatedProducts(page,userId,sortType,searchText,genreFilters.ToList(),typeFilters.ToList());
            var productsCount = await _libraryService.GetCount(userId);
            var pageCount = _libraryService.GetLibraryPageCount(paginatedDatas.DataCount, 8);

            var model = new LibraryPageDto { LibraryProducts = paginatedDatas.Products, PageCount = pageCount };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLibraryIds([FromQuery] string userId)
        {
            return Ok(await _libraryService.GetUserLibraryIds(userId));
        }
    }
}
