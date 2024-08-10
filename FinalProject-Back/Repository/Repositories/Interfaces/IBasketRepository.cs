using Domain.Entities;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        Task<bool> CheckExistUserProduct(int productId, string userId);
        Task<List<Basket>> GetUserBasket(string userId);
        Task<List<int>> GetUserBasketIds(string userId);
        Task<Basket> GetUserBasketProductById(int productId, string userId);
    }
}
