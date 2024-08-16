using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Product
{
    public class SliderProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductLogo {  get; set; }
        public decimal ProductPrice { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ProductType ProductType { get; set; }
    }
}
