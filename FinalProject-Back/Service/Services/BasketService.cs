using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Basket;
using Service.DTOs.Library;
using Service.DTOs.Wallet;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IWalletService _walletService;
        private readonly IProductService _productService;
        private readonly ILibraryService _libraryService;
        private readonly IMapper _mapper;
        public BasketService(IBasketRepository basketRepo,
                             IMapper mapper, 
                             IWalletService walletService,
                             IProductService productService,
                             ILibraryService libraryService)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
            _walletService = walletService;
            _productService = productService;
            _libraryService = libraryService;
        }

        public async Task AddBasket(AddBasketDto model)
        {
            var existData = await _basketRepo.CheckExistUserProduct(model.ProductId, model.UserId);
            if (!existData)
            {
                throw new BadRequestException("Product has already in cart");
            }

            await _basketRepo.Create(_mapper.Map<Basket>(model));
        }

        public async Task BuyProducts(BuyProductDto model)
        {
            var userBalance = await _walletService.GetUserBalance(model.UserId);
            if(model.TotalPrice > userBalance.Balance)
            {
                throw new BadRequestException("Your balance is not enough, please add funds to your wallet");
            }
            await _walletService.RemoveFunds(new WalletDecreaseDto { Balance = userBalance.Balance - model.TotalPrice ,UserId = model.UserId});
            var userBasket = await GetUserBasket(model.UserId);
            await _productService.BuyProducts(userBasket.UserBasket);

            foreach (var item in userBasket.UserBasket)
            {
                await _libraryService.AddLibrary(new AddLibraryDto { UserId = model.UserId , ProductId = item.ProductId });
                await RemoveFromBasket(item);
            }
        }

        public async Task<BasketDto> GetUserBasket(string userId)
        {
            var userBasket = await _basketRepo.GetUserBasket(userId);
            var basketTotal = userBasket.Sum(m => m.Product.ProductPrice);
            return new BasketDto { UserBasket = userBasket,BasketTotal = basketTotal };
        }

        public async Task<List<int>> GetUserBasketIds(string userId)
        {
            return await _basketRepo.GetUserBasketIds(userId);
        }

        public async Task<Basket> GetUserBasketProductById(int productId, string userId)
        {
            return await _basketRepo.GetUserBasketProductById(productId, userId);
        }

        public async Task RemoveFromBasket(Basket basket)
        {
            await _basketRepo.Delete(basket);
        }
    }
}
