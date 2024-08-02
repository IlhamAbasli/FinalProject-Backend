using Service.DTOs.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IWalletService
    {
        Task Create(WalletCreateDto model);
        Task<WalletDto> GetUserBalance(string userId);
    }
}
