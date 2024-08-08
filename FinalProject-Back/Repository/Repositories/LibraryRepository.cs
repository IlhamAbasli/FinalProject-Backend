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

        public async Task<Library> CheckExistUserProduct(int productId, string userId)
        {
            var existData = await _entities.Where(m=>m.UserId == userId).FirstOrDefaultAsync(m=>m.ProductId == productId);
            if(existData is not null)
            {
                throw new BadRequestException("You can only use the same code once.");
            }
            return null;
        }

        public async Task<List<Library>> GetAllPaginatedProducts(int page,string userId, int take = 8)
        {
            return await _entities.Where(m => m.UserId == userId).Include(m => m.Product).ThenInclude(m=>m.ProductImages).Skip((page - 1) * take).Take(take).ToListAsync();
        }

        public async Task<int> GetCount(string userId)
        {
            return await _entities.Where(m => m.UserId == userId).CountAsync();
        }
    }
}
