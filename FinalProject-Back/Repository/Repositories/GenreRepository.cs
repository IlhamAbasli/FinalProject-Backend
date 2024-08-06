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
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Genre>> GetAllWithIncludes()
        {
            return await _entities.Include(m=>m.Products).ThenInclude(m=>m.ProductImages).ToListAsync();
        }
    }
}
