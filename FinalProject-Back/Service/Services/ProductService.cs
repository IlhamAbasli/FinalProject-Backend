using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }




        private static readonly Random random = new Random();
        private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public char GenerateRandomCharacter()
        {
            return characters[random.Next(characters.Length)];
        }

        public string GenerateRedeemCode()
        {
            var sections = new[] { 5, 5, 5, 5 };
            var code = new StringBuilder();

            for (int i = 0; i < sections.Length; i++)
            {
                for (int j = 0; j < sections[i]; j++)
                {
                    code.Append(GenerateRandomCharacter());
                }
                if (i < sections.Length - 1)
                {
                    code.Append('-');
                }
            }

            return code.ToString();
        }

        public async Task Create(Product product)
        {
            await _productRepo.Create(product);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepo.GetAllWithIncludes();
        }
    }
}
