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

        public async Task AddFundsToWallet(Wallet wallet)
        {
            var userWallet = await GetUserBalance(wallet.UserId);
            userWallet.Balance += wallet.Balance;
            await _context.SaveChangesAsync();
        }

        public async Task<Wallet> GetUserBalance(string userId)
        {
            return await _entities.FirstOrDefaultAsync(m => m.UserId == userId);
        }
    }
}
