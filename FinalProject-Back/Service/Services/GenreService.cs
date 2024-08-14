using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Genre;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepo,IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }
        public async Task Create(GenreCreateDto model)
        {
            await _genreRepo.Create(_mapper.Map<Genre>(model));
        }

        public async Task Delete(int id)
        {
            var existGenre = await _genreRepo.GetById(id);
            if (existGenre is null) throw new NotFoundException("Genre not found with this ID");
            await _genreRepo.Delete(existGenre);
        }

        public async Task Edit(int id, GenreEditDto model)
        {
            var existGenre = await _genreRepo.GetById(id);
            if (existGenre is null) throw new NotFoundException("Data not found with this ID");
            _mapper.Map(model, existGenre);
            await _genreRepo.Update(existGenre);
        }

        public async Task<bool> GenreIsExist(string genreName)
        {
            return await _genreRepo.GenreIsExist(genreName);
        }

        public async Task<IEnumerable<GenreDto>> GetAll()
        {
            var genres = await _genreRepo.GetAllWithIncludes();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task<GenreDto> GetById(int id)
        {
            var existGenre = await _genreRepo.GetById(id);
            if (existGenre is null) throw new NotFoundException("Genre not found with this ID");
            return _mapper.Map<GenreDto>(existGenre);
        }
    }
}
