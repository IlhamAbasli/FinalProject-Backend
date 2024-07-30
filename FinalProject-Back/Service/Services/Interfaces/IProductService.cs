using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task Create(Product product);
        char GenerateRandomCharacter();
        string GenerateRedeemCode();
    }
}
