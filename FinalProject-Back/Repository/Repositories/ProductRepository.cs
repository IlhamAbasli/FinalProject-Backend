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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task ChangeMainImage(int productId, int imageId)
        {
            var existData = await _entities.Include(m => m.ProductImages).FirstOrDefaultAsync(m => m.Id == productId);
            foreach (var image in existData.ProductImages)
            {
                image.IsMain = false;
            }

            existData.ProductImages.FirstOrDefault(m => m.Id == imageId).IsMain = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(ProductImage image)
        {
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllWithIncludes()
        {
            return await _entities.Include(m => m.ProductType)
                                  .Include(m => m.ProductImages)
                                  .ToListAsync();
        }

        public async Task<Product> GetByIdWithIncludes(int id)
        {
            return await _entities.Where(m => m.Id == id)
                                  .Include(m => m.ProductType)
                                  .Include(m => m.Genre)
                                  .Include(m => m.ProductImages)
                                  .Include(m => m.SystemRequirements)
                                  .ThenInclude(m=>m.PlatformSystemRequirements)
                                  .ThenInclude(m=>m.Platform)
                                  .Include(m => m.PlatformProducts)
                                  .ThenInclude(m => m.Platform)
                                  .FirstOrDefaultAsync();
        }
        public async Task<Product> GetByRedeemCode(string redeemCode)
        {
            var existData = await _entities.Include(m => m.ProductImages).FirstOrDefaultAsync(m => m.RedeemCode == redeemCode);
            //MNV9Y-AFOVH-4SJPL-OCQ6K
            return existData;
        }
    }
}
