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
    public class PlatformRepository : BaseRepository<Platform> , IPlatformRepository
    {
        public PlatformRepository(AppDbContext context) : base(context) { }
        public async Task<bool> PlatformIsExist(string platformName)
        {
            return await _entities.AnyAsync(m=>m.PlatformName == platformName.Trim());
        }
    }
}
