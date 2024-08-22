using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.DTOs;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Inretfaces;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CheckExistUserProduct(int productId, string userId)
        {
            var existData = await _entities.Where(m=>m.UserId == userId).FirstOrDefaultAsync(m=>m.ProductId == productId);
            if(existData is not null)
            {
                return false;
            }
            return true;
        }

        public async Task<LibraryPaginateDto> GetAllPaginatedProducts(int page,string userId, string sortType, string searchText, List<string> genreFilters, List<string> typeFilters, int take = 8)
        {
            var query = _entities.Where(m => m.UserId == userId)
                                 .Include(m => m.Product)
                                 .ThenInclude(m => m.ProductImages)
                                 .Include(m=>m.Product)
                                 .ThenInclude(m=>m.Genre)
                                 .Include(m => m.Product)
                                 .ThenInclude(m => m.ProductType).AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(p => p.Product.ProductName.ToLower().Contains(searchText.ToLower().Trim()));
            }


            if (genreFilters is not null && genreFilters.Any())
            {
                query = query.Where(m => genreFilters.Contains(m.Product.Genre.GenreName));
            }

            if (typeFilters is not null && typeFilters.Any())
            {
                query = query.Where(m => typeFilters.Contains(m.Product.ProductType.TypeName));
            }

            switch (sortType)
            {
                case "Recently Purchased":
                    query = query.OrderByDescending(m => m.Id);
                    break;
                case "Alphabetical Z-A":
                    query = query.OrderByDescending(m => m.Product.ProductName);
                    break;
                case "Alphabetical A-Z":
                    query = query.OrderBy(m => m.Product.ProductName);
                    break;
                default:
                    break;
            }

            int productCount = query.Count();

            var paginatedDatas = await query.Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync();

            return new LibraryPaginateDto { Products = paginatedDatas, DataCount = productCount };
        }

        public async Task<int> GetCount(string userId)
        {
            return await _entities.Where(m => m.UserId == userId).CountAsync();
        }

        public async Task<List<int>> GetUserLibraryIds(string userId)
        {
            return await _entities.Where(m => m.UserId == userId).Select(m => m.ProductId).ToListAsync();
        }
    }
}
