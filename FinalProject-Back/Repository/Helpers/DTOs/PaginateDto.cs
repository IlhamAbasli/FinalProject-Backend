using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.DTOs
{
    public class PaginateDto
    {
        public List<Product> Products { get; set; }
        public int DataCount { get; set; }
    }
}
