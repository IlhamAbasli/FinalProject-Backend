using Service.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGenreService
    {
        Task Create(GenreCreateDto model);
        Task<IEnumerable<GenreDto>> GetAll();
        Task Delete(int id);
        Task<GenreDto> GetById(int id);
        Task Edit(int id, GenreEditDto model);
    }
}
