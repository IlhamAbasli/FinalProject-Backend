using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.News;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepo;
        private readonly IMapper _mapper;
        public NewsService(INewsRepository newsRepo, IMapper mapper)
        {
            _newsRepo = newsRepo;
            _mapper = mapper;

        }
        public async Task Create(News model)
        {
            await _newsRepo.Create(model);
        }

        public async Task Delete(int id)
        {
            var existData = await _newsRepo.GetById(id);
            if (existData is null) throw new NotFoundException("News not found with this ID");
            await _newsRepo.Delete(existData);
        }

        public async Task Edit(int id, NewsEditDto model)
        {
            var existData = await _newsRepo.GetById(id);
            if (existData is null) throw new NotFoundException("News not found with this ID");
            _mapper.Map(model, existData);
            await _newsRepo.Update(existData);
        }

        public async Task<IEnumerable<NewsDto>> GetAllNews()
        {
            return _mapper.Map<IEnumerable<NewsDto>>(await _newsRepo.GetAll());
        }

        public async Task<IEnumerable<NewsDetailDto>> GetAllNewsWithImages()
        {
            return _mapper.Map<IEnumerable<NewsDetailDto>>(await _newsRepo.FindAllWithIncludes(m => m.NewsImages).ToListAsync());
        }

        public async Task<NewsDetailDto> GetByIdWithImages(int id)
        {
            var existData = await _newsRepo.FindBy(m => m.Id == id, m => m.NewsImages).FirstOrDefaultAsync();
            if (existData is null) throw new NotFoundException("News not found with this ID");
            return _mapper.Map<NewsDetailDto>(existData);
        }


        public async Task<NewsDetailDto> GetById(int id)
        {
            var existData = await _newsRepo.GetById(id);
            if (existData is null) throw new NotFoundException("News not found with this ID");
            return _mapper.Map<NewsDetailDto>(existData);
        }

        public int GetNewsPageCount(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }

        public async Task<List<NewsDetailDto>> GetAllPaginatedNews(int page, int take = 5)
        {
            var news = await _newsRepo.GetAllPaginatedNews(page, take);
            return _mapper.Map<List<NewsDetailDto>>(news);
        }

        public async Task<int> GetCount()
        {
            return await _newsRepo.GetCount();
        }

        public async Task DeleteImage(int imageId, int newsId)
        {
            var existNews = await GetByIdWithImages(newsId);
            if (existNews is null) throw new NotFoundException("News not found with this ID");
            var existImage = existNews.NewsImages.FirstOrDefault(m => m.Id == imageId);
            if (existImage is null) throw new NotFoundException("Data not found with this ID");
            await _newsRepo.DeleteImage(existImage);
        }

        public async Task ChangeMainImage(int newsId, int imageId)
        {
            await _newsRepo.ChangeMainImage(newsId, imageId);
        }
    }
}
