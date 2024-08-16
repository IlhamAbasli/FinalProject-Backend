using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.News;
using Service.DTOs.Product;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }




        private static readonly Random random = new Random();
        private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private char GenerateRandomCharacter()
        {
            return characters[random.Next(characters.Length)];
        }

        public string GenerateRedeemCode()
        {
            var sections = new[] { 5, 5, 5, 5 };
            var code = new StringBuilder();

            for (int i = 0; i < sections.Length; i++)
            {
                for (int j = 0; j < sections[i]; j++)
                {
                    code.Append(GenerateRandomCharacter());
                }
                if (i < sections.Length - 1)
                {
                    code.Append('-');
                }
            }

            return code.ToString();
        }

        public async Task Create(Product product)
        {
            await _productRepo.Create(product);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            return _mapper.Map<List<ProductDto>>(await _productRepo.GetAllWithIncludes());
        }

        public async Task<ProductDetailDto> GetById(int id)
        {
            var existData = await _productRepo.GetByIdWithIncludes(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");

            return _mapper.Map<ProductDetailDto>(existData);   
        }

        public async Task Edit(int id, ProductEditDto model)
        {
            var existData = await _productRepo.GetById(id);
            if(existData is null) throw new NotFoundException("Data not found with this ID");
            _mapper.Map(model,existData);
            await _productRepo.Update(existData);
        }

        public async Task Delete(int id)
        {
            var existData = await _productRepo.GetById(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            await _productRepo.Delete(existData);
        }

        public async Task DeleteImage(int imageId, int productId)
        {
            var existData = await GetById(productId);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            var existImage = existData.ProductImages.FirstOrDefault(m => m.Id == imageId);
            if (existImage is null) throw new NotFoundException("Data not found with this ID");
            await _productRepo.DeleteImage(existImage);
        }

        public async Task ChangeMainImage(int productId, int imageId)
        {
            await _productRepo.ChangeMainImage(productId, imageId);
        }

        public async Task<ProductRedeemDto> GetByRedeemCode(string redeemCode)
        {
            var existProduct = await _productRepo.GetByRedeemCode(redeemCode);
            if (existProduct is null) throw new NotFoundException("Data not found");
            return new ProductRedeemDto { ProductId = existProduct.Id, ProductImages = existProduct.ProductImages };
        }

        public async Task<int> GetCount()
        {
            return await _productRepo.GetCount();
        }


        public int GetProductsPageCount(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }

        public async Task<List<ProductDto>> GetAllPaginatedProducts(int page,string sortType, int take = 12)
        {
            var products = await _productRepo.GetAllPaginatedProducts(page, sortType, take);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task BuyProducts(List<Basket> basket)
        {
            await _productRepo.BuyProducts(basket);
        }

        public async Task<List<SliderProductDto>> GetSliderProducts()
        {
            var products = await _productRepo.GetSliderProducts();
            return _mapper.Map<List<SliderProductDto>> (products);
        }

        public async Task<List<ProductDto>> GetLatestProducts()
        {
            var products = await _productRepo.GetLatestProducts();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetTopSellers()
        {
            var products = await _productRepo.GetTopSellers();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetTrending()
        {
            var products = await _productRepo.GetTrending();
            return _mapper.Map<List<ProductDto>>(products) ;
        }

        public async Task<List<ProductDto>> GetEditorsChoices()
        {
            var products = await _productRepo.GetEditorsChoices();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
