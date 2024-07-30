﻿using Domain.Entities;
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
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<List<Product>> GetAllWithIncludes()
        {
            return await _entities.Include(m=>m.PlatformProducts).ThenInclude(m=>m.Platform).ThenInclude(m=>m.PlatformSystemRequirements).ThenInclude(m=>m.SystemRequirement).ToListAsync();
        }
    }
}
