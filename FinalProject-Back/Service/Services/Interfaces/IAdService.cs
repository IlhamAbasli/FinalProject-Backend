using Domain.Entities;
using Service.DTOs;
using Service.DTOs.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAdService
    {
        Task Create(Advertisement model);
        Task<IEnumerable<AdDto>> GetAll();  
        Task Edit(int id,AdEditDto model);
        Task<AdDto> GetById(int id);
        Task Delete(int id);
    }
}
