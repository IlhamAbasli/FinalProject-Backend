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
                                  .OrderByDescending(m=>m.Id)
                                  .ToListAsync();
        }

        public async Task<Product> GetByIdWithIncludes(int id)
        {
            return await _entities.Where(m => m.Id == id)
                                  .Include(m => m.ProductType)
                                  .Include(m => m.Genre)
                                  .Include(m => m.ProductImages)
                                  .Include(m => m.SystemRequirements)
                                  .ThenInclude(m => m.PlatformSystemRequirements)
                                  .ThenInclude(m => m.Platform)
                                  .Include(m => m.PlatformProducts)
                                  .ThenInclude(m => m.Platform)
                                  .FirstOrDefaultAsync();
        }
        public async Task<Product> GetByRedeemCode(string redeemCode)
        {
            var existData = await _entities.Include(m => m.ProductImages).FirstOrDefaultAsync(m => m.RedeemCode == redeemCode);
            return existData;
        }

        public async Task<List<Product>> GetAllPaginatedProducts(int page, string sortType,string searchText, int take = 12)
        {
            var paginatedDatas = await _entities.Include(m => m.ProductImages).Include(m => m.ProductType).Skip((page - 1) * take).Take(take).ToListAsync();
            switch (sortType)
            {
                case "New Release":
                    paginatedDatas = await _entities.Include(m => m.ProductImages)
                                          .Include(m => m.ProductType)
                                          .OrderByDescending(m => m.Id)
                                          .Skip((page - 1) * take).Take(take)
                                          .ToListAsync();
                    break;
                case "All":
                    break;
                case "Price: High to Low":
                    paginatedDatas = await _entities.Include(m => m.ProductImages)
                                          .Include(m => m.ProductType)
                                          .OrderByDescending(m => m.ProductPrice)
                                          .Skip((page - 1) * take).Take(take)
                                          .ToListAsync();
                    break;
                case "Price: Low to High":
                    paginatedDatas = await _entities.Include(m => m.ProductImages)
                                          .Include(m => m.ProductType)
                                          .OrderBy(m => m.ProductPrice)
                                          .Skip((page - 1) * take).Take(take)
                                          .ToListAsync();
                    break;
                default:
                    break;
            }

            if(searchText is not null)
            {
                paginatedDatas = await _entities.Include(m => m.ProductImages).Include(m => m.ProductType).Where(m=>m.ProductName.ToLower().Contains(searchText.ToLower().Trim())).Skip((page - 1) * take).Take(take).ToListAsync();
                switch (sortType)
                {
                    case "New Release":
                        paginatedDatas = await _entities.Include(m => m.ProductImages)
                                                        .Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim()))
                                                        .Include(m => m.ProductType)
                                                        .OrderByDescending(m => m.Id)
                                                        .Skip((page - 1) * take).Take(take)
                                                        .ToListAsync();
                        break;
                    case "All":
                        break;
                    case "Price: High to Low":
                        paginatedDatas = await _entities.Include(m => m.ProductImages)
                                                        .Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim()))
                                                        .Include(m => m.ProductType)
                                                        .OrderByDescending(m => m.ProductPrice)
                                                        .Skip((page - 1) * take).Take(take)
                                                        .ToListAsync();
                        break;
                    case "Price: Low to High":
                        paginatedDatas = await _entities.Include(m => m.ProductImages)
                                                        .Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim()))
                                                        .Include(m => m.ProductType)
                                                        .OrderBy(m => m.ProductPrice)
                                                        .Skip((page - 1) * take).Take(take)
                                                        .ToListAsync();
                        break;
                    default:
                        break;
                }
            }

            return paginatedDatas;

        }

        public async Task<int> GetCount()
        {
            return await _entities.CountAsync();
        }

        public async Task<int> GetSearchedCount(string searchText)
        {
            var datas = await _entities.Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim())).ToListAsync();
            return datas.Count;
        }


        public async Task BuyProducts(List<Basket> basket)
        {
            foreach (var item in basket)
            {
                var product = await _entities.FirstOrDefaultAsync(m => m.Id == item.ProductId);
                product.Count -= 1;
                product.SellingCount += 1;
                if (product.Count == 0)
                {
                    await Delete(product);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetSliderProducts()
        {
            return await _entities.Include(m => m.ProductImages)
                                  .Include(m => m.ProductType)
                                  .OrderByDescending(m => m.Id)
                                  .Take(6)
                                  .ToListAsync();
        }

        public async Task<List<Product>> GetLatestProducts()
        {
            return await _entities.Include(m => m.ProductImages)
                      .Include(m => m.ProductType)
                      .OrderByDescending(m => m.Id)
                      .Skip(6)
                      .Take(10)
                      .ToListAsync();
        }

        public async Task<List<Product>> GetTopSellers()
        {
            return await _entities.Include(m => m.ProductImages)
                                  .Include(m => m.ProductType)
                                  .OrderByDescending(m => m.SellingCount)
                                  .Take(5)
                                  .ToListAsync();
        }

        public async Task<List<Product>> GetTrending()
        {
            return await _entities.Include(m => m.ProductImages)
                                  .Include(m => m.ProductType)
                                  .OrderByDescending(m => m.ProductName)
                                  .Skip(4)
                                  .Take(5)
                                  .ToListAsync();
        }

        public async Task<List<Product>> GetEditorsChoices()
        {
            return await _entities.Include(m => m.ProductImages)
                      .Include(m => m.ProductType)
                      .OrderByDescending(m => m.ProductPrice)
                      .Skip(3)
                      .Take(5)
                      .ToListAsync();
        }
    }
}
