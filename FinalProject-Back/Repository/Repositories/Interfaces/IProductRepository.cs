using Domain.Entities;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetAllWithIncludes();
        Task<Product> GetByIdWithIncludes(int id);
        Task DeleteImage(ProductImage image);
        Task ChangeMainImage(int productId, int imageId);
    }
}
