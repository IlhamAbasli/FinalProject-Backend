﻿using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<INewsRepository,NewsRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISystemRequirementRepository, SystemRequirementRepository>();
            services.AddScoped<IPlatformProductsRepository, PlatformProductsRepository>();
            services.AddScoped<IPlatformSystemRequirementRepository, PlatformSystemRequirementRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();


            return services;

        }
    }
}
