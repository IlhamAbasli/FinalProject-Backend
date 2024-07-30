using Domain.Entities;
using FinalProject_Back.Helpers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Product;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPlatformService _platformService;
        private readonly ISystemRequirementService _systemRequirementService;
        private readonly IPlatformSystemRequirementService _platformSystemRequirementService;
        private readonly IPlatformProductsService _platformProductsService;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductService productService,
                                 ISystemRequirementService systemRequirementService,
                                 IPlatformSystemRequirementService platformSystemRequirementService,
                                 IPlatformProductsService platformProductsService,
                                 IWebHostEnvironment env,
                                 IPlatformService platformService)
        {
            _env = env;
            _productService = productService;
            _systemRequirementService = systemRequirementService;
            _platformSystemRequirementService = platformSystemRequirementService;
            _platformProductsService = platformProductsService;
            _platformService = platformService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            string productLogoFileName = Guid.NewGuid().ToString() + "-" + request.ProductLogo.FileName;
            string productLogoPath = Path.Combine(_env.WebRootPath, "assets/images", productLogoFileName);
            await request.ProductLogo.SaveFileToLocalAsync(productLogoPath);

            List<ProductImage> images = new();
            foreach (var item in request.ProductImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new ProductImage { ImageName = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            var existPlatform = await _platformService.GetByIdRaw(request.PlatformId);

            Product newProduct = new()
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductImages = images,
                ProductLogo = productLogoFileName,
                ProductTypeId = request.TypeId,
                GenreId = request.GenreId,
                ProductPrice = request.ProductPrice,
                DeveloperName = request.DeveloperName,
                PublisherName = request.PublisherName,
                Count = request.Count,
                PlatformProducts = new List<PlatformProducts>(),
                RedeemCode = _productService.GenerateRedeemCode(),
            };

            newProduct.PlatformProducts.Add(new PlatformProducts
            {
                Product = newProduct,
                Platform = existPlatform,
            });


            var systemRequirement = new SystemRequirement
            {
                MinCpuName = request.MinCpuName,
                MinGpu = request.MinGpu,
                MinMemory = request.MinMemory,
                MinOsVersion = request.MinOsVersion,
                RecomMemory = request.RecomMemory,
                RecomCpuName = request.RecomCpuName,
                RecomGpu = request.RecomGpu,
                RecomOsVersion = request.RecomOsVersion,
            };

            var platformSystemRequirement = new PlatformSystemRequirement
            {
                Platform = existPlatform,
                SystemRequirement = systemRequirement
            };

            await _systemRequirementService.Create(systemRequirement);

            await _platformSystemRequirementService.Create(platformSystemRequirement);

            await _productService.Create(newProduct);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }
    }
}
