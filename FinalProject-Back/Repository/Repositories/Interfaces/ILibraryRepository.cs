using Domain.Entities;
using Repository.Helpers.DTOs;
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
        Task<LibraryPaginateDto> GetAllPaginatedProducts(int page, string userId, string sortType,string searchText, List<string> genreFilters, List<string> typeFilters, int take = 8);
        Task<int> GetCount(string userId);
        Task<List<int>> GetUserLibraryIds(string userId);
    }
}
