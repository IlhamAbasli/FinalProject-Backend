using Domain.Entities;
using FinalProject_Back.Helpers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Helpers.Exceptions;
using Service.DTOs.Ad;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly IWebHostEnvironment _env;
        public AdController(IAdService adService, IWebHostEnvironment env)
        {
            _adService = adService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AdCreateDto request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.AdImage.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
            await request.AdImage.SaveFileToLocalAsync(path);

            await _adService.Create(new Advertisement { AdTitle = request.AdTitle, AdDescription = request.AdDescription, AdImage = fileName });
            return CreatedAtAction(nameof(Create), new { response = "Success" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adService.GetAll());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm] AdEditDto request)
        {
            var existAd = await _adService.GetById((int)id);
            if (existAd is null) throw new NotFoundException("Ad not found with this ID");
            request.AdImage = existAd.AdImage;
            if (request.NewAdImage is not null)
            {
                var newFileName = Guid.NewGuid().ToString() + "-" + request.NewAdImage.FileName;
                var newPath = Path.Combine(_env.WebRootPath, "assets/images", newFileName);
                await request.NewAdImage.SaveFileToLocalAsync(newPath);

                var existPath = Path.Combine(_env.WebRootPath, "assets/images", request.AdImage);
                existPath.DeleteFileFromLocal();
                request.AdImage = newFileName;
            }
            await _adService.Edit((int)id, request);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            return Ok(await _adService.GetById((int)id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            var existAd = await _adService.GetById((int) id);
            var existPath = Path.Combine(_env.WebRootPath,"assets/images",existAd.AdImage);
            existPath.DeleteFileFromLocal();
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            await _adService.Delete((int) id);  
            return Ok();
        }
    }
}
