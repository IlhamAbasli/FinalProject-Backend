using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Product
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; } 
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string DeveloperName { get; set; }
        public string PublisherName { get; set; }
        public IFormFile ProductLogo { get; set; }
        public int Count { get; set; }
        public int GenreId { get; set; }
        public int TypeId { get; set; }
        public int PlatformId { get; set; }
        public string MinOsVersion { get; set; }
        public string MinCpuName { get; set; }
        public string MinMemory { get; set; }
        public string MinGpu { get; set; }
        public string RecomOsVersion { get; set; }
        public string RecomCpuName { get; set; }
        public string RecomMemory { get; set; }
        public string RecomGpu { get; set; }
        public List<IFormFile> ProductImages { get; set; }
    }
}
