using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Wallet
{
    public class WalletDecreaseDto
    {
        public decimal Balance { get; set; }
        public string UserId { get; set; }
    }
}
