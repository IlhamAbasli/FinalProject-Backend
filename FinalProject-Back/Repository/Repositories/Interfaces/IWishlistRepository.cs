using Domain.Entities;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IWishlistRepository : IBaseRepository<Wishlist>
    {
        Task<bool> CheckExistUserProduct(int productId, string userId);
        Task<List<Wishlist>> GetUserWishlist(string userId);    
        Task<List<int>> GetUserWishlistIds(string userId);
        Task<Wishlist> GetUserWishlistProductById(int productId, string userId);


    }
}
