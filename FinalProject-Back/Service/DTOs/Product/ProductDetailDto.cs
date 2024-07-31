using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Product
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string DeveloperName { get; set; }
        public string PublisherName { get; set; }
        public int Count { get; set; }
        public string ProductLogo { get; set; }
        public string RedeemCode { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ProductType ProductType { get; set; }
        public Domain.Entities.Genre Genre { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<PlatformProducts> PlatformProducts { get; set; }
        public ICollection<SystemRequirement> SystemRequirements { get; set; }
    }
}
