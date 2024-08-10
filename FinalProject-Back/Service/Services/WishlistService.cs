using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Wishlist;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WishlistService : IWishlishtService
    {
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IMapper _mapper;
        public WishlistService(IWishlistRepository wishlistRepo,IMapper mapper)
        {
            _wishlistRepo = wishlistRepo;
            _mapper = mapper;
        }

        public async Task AddWishlist(AddWishlistDto model)
        {
            var existData = await _wishlistRepo.CheckExistUserProduct(model.ProductId,model.UserId);
            if (!existData)
            {
                throw new BadRequestException("Product has already wishlisted");
            }

            await _wishlistRepo.Create(_mapper.Map<Wishlist>(model));
        }

        public async Task<List<Wishlist>> GetUserWishlist(string userId)
        {
            return await _wishlistRepo.GetUserWishlist(userId);
        }

        public async Task<List<int>> GetUserWishlistIds(string userId)
        {
            return await _wishlistRepo.GetUserWishlistIds(userId);
        }

        public async Task<Wishlist> GetUserWishlistProductById(int productId, string userId)
        {
            return await _wishlistRepo.GetUserWishlistProductById(productId, userId);
        }

        public async Task RemoveFromWishlist(Wishlist wishlist)
        {
            await _wishlistRepo.Delete(wishlist);
        }
    }
}
