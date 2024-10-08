﻿using Domain.Entities;
using Repository.Helpers.DTOs;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetAllWithIncludes();
        Task<Product> GetByIdWithIncludes(int id);
        Task DeleteImage(ProductImage image);
        Task ChangeMainImage(int productId, int imageId);
        Task<Product> GetByRedeemCode(string redeemCode);
        Task<PaginateDto> GetAllPaginatedProducts(int page,string sortType,string searchText, List<string> priceFilters, List<string> genreFilters, List<string> typeFilters, int take = 12);
        Task<List<Product>> GetSliderProducts();
        Task<List<Product>> GetLatestProducts();
        Task<List<Product>> GetTopSellers();
        Task<List<Product>> GetTrending();
        Task<List<Product>> GetEditorsChoices();
        Task<int> GetCount();
        Task<int> GetSearchedCount(string searchText);
        Task BuyProducts(List<Basket> basket);
    }
}
