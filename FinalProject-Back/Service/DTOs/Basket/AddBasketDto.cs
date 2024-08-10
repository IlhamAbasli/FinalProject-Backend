using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Basket
{
    public class AddBasketDto
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }  
    }
}
