using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Helpers.Exceptions;
using Service.DTOs.Type;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TypeCreateDto request)
        {
            await _typeService.Create(request);
            return CreatedAtAction(nameof(Create), new { response = "Success" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _typeService.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            await _typeService.Delete((int)id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm] TypeEditDto request)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            await _typeService.Edit((int)id, request);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            return Ok(await _typeService.GetById((int)id));
        }

    }
}
