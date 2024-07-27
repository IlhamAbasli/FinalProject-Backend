using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Wallet> Wallets { get; set; }

    }
}
