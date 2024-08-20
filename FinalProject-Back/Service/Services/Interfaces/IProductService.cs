using Domain.Entities;
using Service.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductDetailDto> GetById(int id);
        Task Create(Product product);
        Task Edit(int id, ProductEditDto model);
        Task Delete(int id);
        Task DeleteImage(int imageId, int productId);
        Task ChangeMainImage(int productId, int imageId);
        string GenerateRedeemCode();
        Task<ProductRedeemDto> GetByRedeemCode(string redeemCode);
        int GetProductsPageCount(int count, int take);
        Task<List<ProductDto>> GetAllPaginatedProducts(int page, string sortType,string searchText, List<string> priceFilters, List<string> genreFilters, List<string> typeFilters, int take = 12);
        Task<int> GetCount();
        Task<int> GetSearchedCount(string searchText);
        Task BuyProducts(List<Basket> basket);
        Task<List<SliderProductDto>> GetSliderProducts();
        Task<List<ProductDto>> GetLatestProducts();
        Task<List<ProductDto>> GetTopSellers();
        Task<List<ProductDto>> GetEditorsChoices();
        Task<List<ProductDto>> GetTrending();
    }
}
