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
    public class WishlistRepository : BaseRepository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckExistUserProduct(int productId, string userId)
        {
            var existData = await _entities.Where(m => m.UserId == userId).FirstOrDefaultAsync(m => m.ProductId == productId);
            if (existData is not null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Wishlist>> GetUserWishlist(string userId)
        {
            return await _entities.Where(m=>m.UserId == userId).Include(m=>m.Product).ThenInclude(m=>m.ProductImages).ToListAsync();
        }

        public async Task<List<int>> GetUserWishlistIds(string userId)
        {
            return await _entities.Where(m=>m.UserId == userId).Select(m=>m.ProductId).ToListAsync();
        }

        public async Task<Wishlist> GetUserWishlistProductById(int productId,string userId)
        {
            return await _entities.FirstOrDefaultAsync(m=>m.UserId == userId && m.ProductId == productId);
        }
    }
}
