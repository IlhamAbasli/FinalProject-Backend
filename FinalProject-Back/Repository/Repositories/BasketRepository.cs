using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CheckExistUserProduct(int productId, string userId)
        {
            var existData = await _entities.Where(m => m.UserId == userId).FirstOrDefaultAsync(m => m.ProductId == productId);
            if (existData is not null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Basket>> GetUserBasket(string userId)
        {
            return await _entities.Where(m => m.UserId == userId).Include(m => m.Product).ThenInclude(m => m.ProductImages).ToListAsync();
        }

        public async Task<List<int>> GetUserBasketIds(string userId)
        {
            return await _entities.Where(m => m.UserId == userId).Select(m => m.ProductId).ToListAsync();
        }

        public async Task<Basket> GetUserBasketProductById(int productId, string userId)
        {
            return await _entities.FirstOrDefaultAsync(m => m.UserId == userId && m.ProductId == productId);
        }
    }
}
