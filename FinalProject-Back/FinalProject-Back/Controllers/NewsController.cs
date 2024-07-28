using Domain.Entities;
using FinalProject_Back.Helpers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Repository.Helpers.Exceptions;
using Service.DTOs.News;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IWebHostEnvironment _env;
        public NewsController(INewsService newsService,IWebHostEnvironment env)
        {
            _newsService = newsService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewsCreateDto request)
        {
            List<NewsImageDto> images = new();
            if(request.Images != null || request.Images.Count > 0)
            {
                foreach(var image in request.Images)
                {
                    var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                    var path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                    await image.SaveFileToLocalAsync(path);
                    images.Add(new NewsImageDto { ImageName = fileName});
                }
            }
            images.FirstOrDefault().IsMain = true;

            await _newsService.Create(new News { NewsTitle = request.Title,NewsImages = images.Select(m=> new NewsImage { Image = m.ImageName, IsMain = m.IsMain }).ToList(), NewsContent1 = request.NewsContent1, NewsContent2 = request.NewsContent2, NewsContent3 = request.NewsContent3 });

            return CreatedAtAction(nameof(Create),new {response = "Success"});
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var datas = await _newsService.GetAllNews();
            return Ok(datas);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] int page = 1)
        {
            var paginatedDatas = await _newsService.GetAllPaginatedNews(page);
            var newsCount = await _newsService.GetCount();
            var pageCount = _newsService.GetNewsPageCount(newsCount, 5);

            var model = new NewsPageDto { News = paginatedDatas, PageCount = pageCount };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithImages()
        {
            return Ok(await _newsService.GetAllNewsWithImages());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existNews = await _newsService.GetByIdWithImages(id);
            foreach (var item in existNews.NewsImages)
            {
                var path = Path.Combine(_env.WebRootPath, "assets/images", item.Image);
                path.DeleteFileFromLocal();
            }
            await _newsService.Delete(id);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t be empty");
            var existNews = await _newsService.GetByIdWithImages((int)id);
            return Ok(existNews);
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm] NewsEditDto request)
        {
            var existNews = await _newsService.GetByIdWithImages((int)id);
            request.NewsImages = existNews.NewsImages;
            if(request.NewImages is not null)
            {
                foreach (var image in request.NewImages)
                {
                    var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                    var path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                    await image.SaveFileToLocalAsync(path);
                    request.NewsImages.Add(new NewsImage { Image = fileName});
                }
            }

            await _newsService.Edit((int) id, request); 
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery] int? imageId, [FromQuery] int? newsId)
        {
            var existNews = await _newsService.GetByIdWithImages((int)newsId);
            if (existNews is null) throw new NotFoundException("News not found with this ID");
            var existImage = existNews.NewsImages.FirstOrDefault(m=>m.Id == (int)imageId);
            if (existImage is null) throw new NotFoundException("Image not found with this ID");

            string path = Path.Combine(_env.WebRootPath, "assets/images", existImage.Image);
            path.DeleteFileFromLocal();

            await _newsService.DeleteImage((int)imageId,(int)newsId);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> ChangeMainImage([FromQuery] int? imageId, [FromQuery] int? newsId)
        {
            await _newsService.ChangeMainImage((int)newsId,(int)imageId);
            return Ok();
        }
    }
}
