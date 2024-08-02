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
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context) { }

        public async Task<List<Wallet>> GetUserBalance(string userId)
        {
            return await _entities.Where(m => m.UserId == userId).ToListAsync();
        }
    }
}
