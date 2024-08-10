using Domain.Entities;
using Repository.Repositories.Inretfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ILibraryRepository : IBaseRepository<Library>
    {
        Task<bool> CheckExistUserProduct(int productId,string userId);
        Task<List<Library>> GetAllPaginatedProducts(int page, string userId, int take = 8);
        Task<int> GetCount(string userId);
        Task<List<int>> GetUserLibraryIds(string userId);
    }
}
