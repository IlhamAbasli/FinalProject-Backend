using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Wishlist
{
    public class RemoveWishlistDto
    {
        public string UserId { get; set; } 
        public int ProductId {  get; set; }
    }
}
