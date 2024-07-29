using Service.DTOs.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITypeService
    {
        Task Create(TypeCreateDto model);
        Task<IEnumerable<TypeDto>> GetAll();
        Task Delete(int id);
        Task<TypeDto> GetById(int id);
        Task Edit(int id,TypeEditDto model);
    }
}
