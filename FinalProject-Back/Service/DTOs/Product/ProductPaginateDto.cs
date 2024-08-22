using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Product
{
    public class ProductPaginateDto
    {
        public List<ProductDto> Products { get; set; }
        public int DataCount { get; set; }
    }
}
