using AutoMapper;
using Domain.Entities;
using Service.DTOs.Account;
using Service.DTOs.Ad;
using Service.DTOs.Genre;
using Service.DTOs.Library;
using Service.DTOs.News;
using Service.DTOs.Platform;
using Service.DTOs.Product;
using Service.DTOs.Type;
using Service.DTOs.Wallet;
using Service.DTOs.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsDto>();
            CreateMap<NewsEditDto, News>();
            CreateMap<News,NewsDetailDto>();

            CreateMap<GenreCreateDto, Genre>();
            CreateMap<Genre,GenreDto>();
            CreateMap<GenreEditDto, Genre>();

            CreateMap<TypeCreateDto, ProductType>();
            CreateMap<ProductType,TypeDto>();
            CreateMap<TypeEditDto, ProductType>();

            CreateMap<Advertisement,AdDto>();
            CreateMap<AdEditDto, Advertisement>();

            CreateMap<Platform, PlatformDto>();
            CreateMap<PlatformEditDto, Platform>();

            CreateMap<Product,ProductDto>();
            CreateMap<Product, ProductDetailDto>();
            CreateMap<ProductEditDto,Product>();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<WalletCreateDto, Wallet>();

            CreateMap<UserUpdateDto,AppUser>();

            CreateMap<AddLibraryDto,Library>();

            CreateMap<AddWishlistDto,Wishlist>();
        }

    }
}
