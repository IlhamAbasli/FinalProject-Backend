using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
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

        public async Task<List<Library>> GetAllPaginatedProducts(int page,string userId, string sortType, int take = 8)
        {
            var paginatedDatas = await _entities.Where(m => m.UserId == userId).Include(m => m.Product).ThenInclude(m => m.ProductImages).Skip((page - 1) * take).Take(take).ToListAsync();
            switch (sortType)
            {
                case "Recently Purchased":
                    return await _entities.Where(m => m.UserId == userId).Include(m => m.Product).ThenInclude(m => m.ProductImages).OrderByDescending(m => m.Id).Skip((page - 1) * take).Take(take).ToListAsync();
                case "All":
                    return paginatedDatas;
                case "Alphabetical A-Z":
                    return await _entities.Where(m => m.UserId == userId).Include(m => m.Product).ThenInclude(m => m.ProductImages).OrderByDescending(m => m.Product.ProductName).Skip((page - 1) * take).Take(take).ToListAsync();
                case "Alphabetical Z-A":
                    return await _entities.Where(m => m.UserId == userId).Include(m => m.Product).ThenInclude(m => m.ProductImages).OrderBy(m => m.Product.ProductName).Skip((page - 1) * take).Take(take).ToListAsync();
                default:
                    break;
            }
            return paginatedDatas;
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
