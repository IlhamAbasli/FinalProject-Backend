using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Basket;
using Service.DTOs.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddBasket(AddBasketDto model);
        Task<BasketDto> GetUserBasket(string userId);
        Task<List<int>> GetUserBasketIds(string userId);
        Task<Basket> GetUserBasketProductById(int productId, string userId);
        Task RemoveFromBasket(Basket basket);
        Task BuyProducts(BuyProductDto model);
    }
}
