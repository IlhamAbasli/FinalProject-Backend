using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context) { }

        public async Task ChangeMainImage(int newsId,int imageId)
        {
            var existNews = await _entities.Include(m=>m.NewsImages).FirstOrDefaultAsync(m=>m.Id == newsId);
            if (existNews is null) throw new NotFoundException("Data not found with this ID");
            foreach (var image in existNews.NewsImages)
            {
                image.IsMain = false;
            }

            existNews.NewsImages.FirstOrDefault(m=>m.Id == imageId).IsMain = true;  
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(NewsImage image)
        {
            _context.NewsImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<News>> GetAllPaginatedNews(int page, int take = 5)
        {
            return await _entities.Include(m=>m.NewsImages).Skip((page - 1) * take).Take(take).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _entities.CountAsync();
        }
    }
}
