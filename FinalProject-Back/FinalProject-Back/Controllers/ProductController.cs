using Domain.Entities;
using FinalProject_Back.Helpers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Helpers.Exceptions;
using Service.DTOs.News;
using Service.DTOs.Product;
using Service.Services;
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
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductService productService,
                                 ISystemRequirementService systemRequirementService,
                                 IPlatformSystemRequirementService platformSystemRequirementService,
                                 IWebHostEnvironment env,
                                 IPlatformService platformService)
        {
            _env = env;
            _productService = productService;
            _systemRequirementService = systemRequirementService;
            _platformSystemRequirementService = platformSystemRequirementService;
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

            List<SystemRequirement> systemRequirements = new();

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

            systemRequirements.Add(systemRequirement);



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
                SystemRequirements = systemRequirements,
                RedeemCode = _productService.GenerateRedeemCode(),
            };

            newProduct.PlatformProducts.Add(new PlatformProducts
            {
                Product = newProduct,
                Platform = existPlatform,
            });


            var platformSystemRequirement = new PlatformSystemRequirement
            {
                Platform = existPlatform,
                SystemRequirement = systemRequirement
            };


            await _platformSystemRequirementService.Create(platformSystemRequirement);
            await _productService.Create(newProduct);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm] ProductEditDto request)
        {
            if (id is null) throw new BadRequestException("ID cant leave empty");
            var existData = await _productService.GetById((int)id);
            request.ProductLogo = existData.ProductLogo;
            request.ProductImages = existData.ProductImages;


            if (request.NewProductLogo is not null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "-" + request.NewProductLogo.FileName;
                string logoPath = Path.Combine(_env.WebRootPath, "assets/images", logoFileName);
                await request.NewProductLogo.SaveFileToLocalAsync(logoPath);

                string oldLogoPath = Path.Combine(_env.WebRootPath, "assets/images", existData.ProductLogo);
                oldLogoPath.DeleteFileFromLocal();
                request.ProductLogo = logoFileName;
            }

            if (request.NewProductImages is not null)
            {
                foreach (var item in request.NewProductImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                    string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                    await item.SaveFileToLocalAsync(path);
                    request.ProductImages.Add(new ProductImage { ImageName = fileName });
                }
            }

            request.SystemRequirements = existData.SystemRequirements;
            foreach (var item in request.SystemRequirements)
            {
                item.MinOsVersion = request.MinOsVersion;
                item.MinCpuName = request.MinCpuName;
                item.MinMemory = request.MinMemory;
                item.MinGpu = request.MinGpu;
                item.RecomOsVersion = request.RecomOsVersion;
                item.RecomGpu = request.RecomGpu;
                item.RecomCpuName = request.RecomCpuName;
                item.RecomMemory = request.RecomMemory;
            }

            request.PlatformProducts = existData.PlatformProducts;
            foreach (var item in request.PlatformProducts)
            {
                item.PlatformId = request.PlatformId;
            }

            await _productService.Edit((int)id, request);   
            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            return Ok(await _productService.GetById((int)id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery] int? imageId, [FromQuery] int? productId)
        {
            var existData = await _productService.GetById((int)productId);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            var existImage = existData.ProductImages.FirstOrDefault(m => m.Id == (int)imageId);
            if (existImage is null) throw new NotFoundException("Image not found with this ID");

            string path = Path.Combine(_env.WebRootPath, "assets/images", existImage.ImageName);
            path.DeleteFileFromLocal();

            await _productService.DeleteImage((int)imageId, (int)productId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeMainImage([FromQuery] int? imageId, [FromQuery] int? productId)
        {
            await _productService.ChangeMainImage((int)productId, (int)imageId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id is null) throw new BadRequestException("ID can`t leave empty");
            var existData = await _productService.GetById((int)id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            string oldLogoPath = Path.Combine(_env.WebRootPath, "assets/images", existData.ProductLogo);
            oldLogoPath.DeleteFileFromLocal();

            foreach (var item in existData.ProductImages)
            {
                string oldPath = Path.Combine(_env.WebRootPath, "assets/images", item.ImageName);
                oldLogoPath.DeleteFileFromLocal();
            }

            await _productService.Delete((int)id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetByRedeem([FromQuery] string redeemCode)
        {
            var product = await _productService.GetByRedeemCode(redeemCode);
            return Ok(product);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] string sortType, [FromQuery] string searchText, [FromQuery] string[] priceFilters, [FromQuery] string[] genreFilters, [FromQuery] string[] typeFilters, [FromQuery] int page = 1)
        {
            var paginatedDatas = await _productService.GetAllPaginatedProducts(page,sortType,searchText,priceFilters.ToList(),genreFilters.ToList(),typeFilters.ToList());
            var pageCount = _productService.GetProductsPageCount(paginatedDatas.DataCount, 12);

            var model = new ProductsPageDto { Products = paginatedDatas.Products, PageCount = pageCount };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetSliderProducts()
        {
            return Ok(await _productService.GetSliderProducts());
        }
        [HttpGet]
        public async Task<IActionResult> GetLatestProducts()
        {
            return Ok(await _productService.GetLatestProducts());
        }

        [HttpGet]
        public async Task<IActionResult> GetTopSellers()
        {
            return Ok(await _productService.GetTopSellers());
        }

        [HttpGet]
        public async Task<IActionResult> GetTrending()
        {
            return Ok(await _productService.GetTrending());
        }

        [HttpGet]
        public async Task<IActionResult> GetEditorsChoices()
        {
            return Ok(await _productService.GetEditorsChoices());
        }
    }

}
