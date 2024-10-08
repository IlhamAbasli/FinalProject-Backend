﻿using Domain.Entities;
using Service.DTOs.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ILibraryService
    {
        Task AddLibraryByRedeem(AddLibraryDto model);
        Task<LibraryPaginateDto> GetAllPaginatedProducts(int page, string userId,string sortType, string searchText, List<string> genreFilters, List<string> typeFilters, int take = 8);
        int GetLibraryPageCount(int count, int take);
        Task<int> GetCount(string userId);
        Task<List<int>> GetUserLibraryIds(string userId);
        Task AddLibrary(AddLibraryDto model);
    }
}
