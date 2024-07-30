using Domain.Entities;
using FinalProject_Back.Helpers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Helpers.Exceptions;
using Service.DTOs.Platform;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        private readonly IWebHostEnvironment _env;
        public PlatformController(IPlatformService platformService, IWebHostEnvironment env)
        {
            _platformService = platformService; 
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PlatformCreateDto request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.PlatformLogo.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
            await request.PlatformLogo.SaveFileToLocalAsync(path);

            await _platformService.Create(new Platform { PlatformName = request.PlatformName, PlatformLogo = fileName });
            return CreatedAtAction(nameof(Create), new {response = "Success"});
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _platformService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            return Ok(await _platformService.GetById((int)id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");

            var existData = await _platformService.GetById((int)id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            string path = Path.Combine(_env.WebRootPath, "assets/images", existData.PlatformLogo);
            path.DeleteFileFromLocal();

            await _platformService.Delete((int) id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm] PlatformEditDto request)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            var existData = await _platformService.GetById((int)id);
            if (existData is null) throw new NotFoundException("Ad not found with this ID");

            request.PlatformLogo = existData.PlatformLogo;
            if(request.NewPlatformLogo is not null)
            {
                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewPlatformLogo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/images", newFileName);
                await request.NewPlatformLogo.SaveFileToLocalAsync(newPath);

                request.PlatformLogo = newFileName;
                string oldPath = Path.Combine(_env.WebRootPath, "assets/images", existData.PlatformLogo);
                oldPath.DeleteFileFromLocal();
            }

            await _platformService.Edit((int) id, request); 
            return Ok();
        }
    }
}
