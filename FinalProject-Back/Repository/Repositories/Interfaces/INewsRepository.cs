using Domain.Entities;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface INewsRepository : IBaseRepository<News>
    {
        Task<List<News>> GetAllPaginatedNews(int page, int take = 5);
        Task<int> GetCount();

        Task DeleteImage(NewsImage image);
        Task ChangeMainImage(int newsId,int imageId);
        Task<List<News>> GetLatestNews();

    }
}
