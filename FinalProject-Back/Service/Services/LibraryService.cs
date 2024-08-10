using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Library;
using Service.DTOs.Product;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LibraryService : ILibraryService
    { 
        private readonly ILibraryRepository _libraryRepo;
        private readonly IMapper _mapper;
        public LibraryService(ILibraryRepository libraryRepo,IMapper mapper)
        {
            _libraryRepo = libraryRepo; 
            _mapper = mapper;
        }


        public async Task AddLibraryByRedeem(AddLibraryDto model)
        {
            var existData = await _libraryRepo.CheckExistUserProduct(model.ProductId,model.UserId);
            if (!existData)
            {
                throw new BadRequestException("You can only use the same code once.");
            }
            await _libraryRepo.Create(_mapper.Map<Library>(model));
        }

        public int GetLibraryPageCount(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }

        public async Task<List<Library>> GetAllPaginatedProducts(int page,string userId, int take = 8)
        {
            var products = await _libraryRepo.GetAllPaginatedProducts(page,userId, take);
            return products;
        }

        public async Task<int> GetCount(string userId)
        {
            return await _libraryRepo.GetCount(userId);
        }
    }
}
