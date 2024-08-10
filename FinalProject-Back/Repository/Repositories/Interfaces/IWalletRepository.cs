using Domain.Entities;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<Wallet> GetUserBalance(string userId);
        Task AddFundsToWallet(Wallet wallet);
        Task RemoveFundsFromWallet(Wallet wallet);
    }
}
