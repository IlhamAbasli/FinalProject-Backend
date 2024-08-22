using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.DTOs;
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
                                  .OrderByDescending(m => m.Id)
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

        //public async Task<List<Product>> GetAllPaginatedProducts(int page, string sortType, string searchText, List<string> priceFilters, List<string> genreFilters, List<string> typeFilters, int take = 12)
        //{
        //    var paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                        .Include(m => m.ProductType)
        //                                        .Include(m=>m.Genre)
        //                                        .Skip((page - 1) * take).Take(take).ToListAsync();


        //    switch (sortType)
        //    {
        //        case "New Release":
        //            paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                  .Include(m => m.ProductType)
        //                                  .OrderByDescending(m => m.Id)
        //                                  .Skip((page - 1) * take).Take(take)
        //                                  .ToListAsync();
        //            break;
        //        case "All":
        //            break;
        //        case "Price: High to Low":
        //            paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                  .Include(m => m.ProductType)
        //                                  .OrderByDescending(m => m.ProductPrice)
        //                                  .Skip((page - 1) * take).Take(take)
        //                                  .ToListAsync();
        //            break;
        //        case "Price: Low to High":
        //            paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                  .Include(m => m.ProductType)
        //                                  .OrderBy(m => m.ProductPrice)
        //                                  .Skip((page - 1) * take).Take(take)
        //                                  .ToListAsync();
        //            break;
        //        default:
        //            break;
        //    }

        //    if (searchText is not null)
        //    {
        //        paginatedDatas = await _entities.Include(m => m.ProductImages).Include(m => m.ProductType).Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim())).Skip((page - 1) * take).Take(take).ToListAsync();
        //        switch (sortType)
        //        {
        //            case "New Release":
        //                paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                                .Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim()))
        //                                                .Include(m => m.ProductType)
        //                                                .OrderByDescending(m => m.Id)
        //                                                .Skip((page - 1) * take).Take(take)
        //                                                .ToListAsync();
        //                break;
        //            case "All":
        //                break;
        //            case "Price: High to Low":
        //                paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                                .Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim()))
        //                                                .Include(m => m.ProductType)
        //                                                .OrderByDescending(m => m.ProductPrice)
        //                                                .Skip((page - 1) * take).Take(take)
        //                                                .ToListAsync();
        //                break;
        //            case "Price: Low to High":
        //                paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                                .Where(m => m.ProductName.ToLower().Contains(searchText.ToLower().Trim()))
        //                                                .Include(m => m.ProductType)
        //                                                .OrderBy(m => m.ProductPrice)
        //                                                .Skip((page - 1) * take).Take(take)
        //                                                .ToListAsync();
        //                break;
        //            default:
        //                break;
        //        }
        //    }


        //    if (genreFilters.FirstOrDefault() is not null)
        //    {
        //        paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                      .Include(m => m.ProductType)
        //                                      .Include(m => m.Genre)
        //                                      .Where(m => genreFilters.Contains(m.Genre.GenreName))
        //                                      .Skip((page - 1) * take).Take(take)
        //                                      .ToListAsync();
        //        switch (sortType)
        //        {
        //            case "New Release":
        //                paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                      .Include(m => m.ProductType)
        //                                      .Include(m => m.Genre)
        //                                      .Where(m => genreFilters.Contains(m.Genre.GenreName))
        //                                      .OrderByDescending(m => m.Id)
        //                                      .Skip((page - 1) * take).Take(take)
        //                                      .ToListAsync();
        //                break;
        //            case "All":
        //                break;
        //            case "Price: High to Low":
        //                paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                      .Include(m => m.ProductType)
        //                                      .Include(m => m.Genre)
        //                                      .Where(m => genreFilters.Contains(m.Genre.GenreName))
        //                                      .OrderByDescending(m => m.ProductPrice)
        //                                      .Skip((page - 1) * take).Take(take)
        //                                      .ToListAsync();
        //                break;
        //            case "Price: Low to High":
        //                paginatedDatas = await _entities.Include(m => m.ProductImages)
        //                                      .Include(m => m.ProductType)
        //                                      .Include(m => m.Genre)
        //                                      .Where(m => genreFilters.Contains(m.Genre.GenreName))
        //                                      .OrderBy(m => m.ProductPrice)
        //                                      .Skip((page - 1) * take).Take(take)
        //                                      .ToListAsync();
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return paginatedDatas;

        //}
        public async Task<PaginateDto> GetAllPaginatedProducts(int page, string sortType, string searchText, List<string> priceFilters, List<string> genreFilters, List<string> typeFilters, int take = 12)
        {
            var query = _entities.Include(m => m.ProductImages)
                                 .Include(m => m.ProductType)
                                 .Include(m => m.Genre)
                                 .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(searchText.ToLower().Trim()));
            }

            if (priceFilters is not null && priceFilters.Any())
            {
                foreach (var price in priceFilters)
                {
                    switch (price)
                    {
                        case "Free":
                            query = query.Where(p => p.ProductPrice == 0);
                            break;
                        case "Under $5.00":
                            query = query.Where(p => p.ProductPrice < 5);
                            break;
                        case "Under $10.00":
                            query = query.Where(p => p.ProductPrice < 10);
                            break;
                        case "Under $20.00":
                            query = query.Where(p => p.ProductPrice < 20);
                            break;
                        case "Under $30.00":
                            query = query.Where(p => p.ProductPrice < 30);
                            break;
                        case "$14.00 and above":
                            query = query.Where(p => p.ProductPrice >= 14);
                            break;
                    }
                }
            }

            if (genreFilters is not null && genreFilters.Any())
            {
                query = query.Where(m => genreFilters.Contains(m.Genre.GenreName));
            }

            if (typeFilters is not null && typeFilters.Any())
            {
                query = query.Where(m => typeFilters.Contains(m.ProductType.TypeName));
            }

            switch (sortType)
            {
                case "New Release":
                    query = query.OrderByDescending(m => m.Id);
                    break;
                case "Price: High to Low":
                    query = query.OrderByDescending(m => m.ProductPrice);
                    break;
                case "Price: Low to High":
                    query = query.OrderBy(m => m.ProductPrice);
                    break;
                default:
                    break;
            }

            int productCount = query.Count();

            var paginatedDatas = await query.Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync();

            return new PaginateDto { Products = paginatedDatas, DataCount = productCount };
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
