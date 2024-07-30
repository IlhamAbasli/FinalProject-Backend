using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IAdService, AdService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISystemRequirementService, SystemRequirementService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<IPlatformSystemRequirementService, PlatformSystemRequirementService>();
            services.AddScoped<IPlatformProductsService, PlatformProductsService>();

            return services;
        }
    }
}
