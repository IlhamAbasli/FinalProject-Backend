using Domain.Entities;
using Service.DTOs.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IWishlishtService
    {
        Task AddWishlist(AddWishlistDto model);
        Task<List<Wishlist>> GetUserWishlist(string userId);
        Task<List<int>> GetUserWishlistIds(string userId);
        Task<Wishlist> GetUserWishlistProductById(int productId, string userId);
        Task RemoveFromWishlist(Wishlist wishlist); 
    }
}
