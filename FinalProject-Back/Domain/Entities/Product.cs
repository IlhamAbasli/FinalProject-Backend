using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string DeveloperName { get; set; }
        public string PublisherName { get; set; }
        public string ProductLogo { get; set; }
        public int Count { get; set; } = 0;
        public int SellingCount { get; set; } = 0;
        public string RedeemCode { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<PlatformProducts> PlatformProducts { get; set; }
        public ICollection<SystemRequirement> SystemRequirements { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
