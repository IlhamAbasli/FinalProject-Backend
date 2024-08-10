using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Basket
{
    public class BasketDto
    {
        public List<Domain.Entities.Basket> UserBasket {  get; set; }   
        public decimal BasketTotal { get; set; }
    }
}
