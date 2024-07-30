using Domain.Entities;
using Service.DTOs.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IPlatformService
    {
        Task Create(Platform model);
        Task<IEnumerable<PlatformDto>> GetAll();
        Task<PlatformDto> GetById(int id);
        Task<Platform> GetByIdRaw(int id);

        Task Delete(int id);

        Task Edit(int id ,PlatformEditDto model);
    }
}
