using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Helpers.Exceptions;
using Service.DTOs.Genre;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GenreCreateDto request)
        {
            await _genreService.Create(request);
            return CreatedAtAction(nameof(Create), new { response = "Success" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAll();
            return Ok(genres);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            await _genreService.Delete((int)id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");

            return Ok(await _genreService.GetById((int)id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm]  GenreEditDto request)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");

            await _genreService.Edit((int)id, request);
            return Ok();
        }    
    }
}
