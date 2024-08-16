using Domain.Entities;
using Service.DTOs.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNews();
        Task<NewsDetailDto> GetById(int id);
        Task Create(News model);
        Task Delete(int id);
        Task Edit(int id,NewsEditDto model);
        Task<IEnumerable<NewsDetailDto>> GetAllNewsWithImages();
        Task<NewsDetailDto> GetByIdWithImages(int id);
        int GetNewsPageCount(int count, int take);
        Task<List<NewsDetailDto>> GetAllPaginatedNews(int page, int take = 5);
        Task DeleteImage(int imageId, int newsId);
        Task ChangeMainImage(int newsId,int imageId);
        Task<List<NewsDetailDto>> GetLatestNews();

        Task<int> GetCount();

    }
}
