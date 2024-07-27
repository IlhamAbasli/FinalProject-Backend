using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
