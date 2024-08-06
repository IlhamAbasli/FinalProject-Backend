using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Platform;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepo;
        private readonly IMapper _mapper;
        public PlatformService(IPlatformRepository platformRepo,IMapper mapper)
        {
            _mapper = mapper;
            _platformRepo = platformRepo;
        }
        public async Task Create(Platform model)
        {
            await _platformRepo.Create(model);
        }

        public async Task Delete(int id)
        {
            var existData = await _platformRepo.GetById(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            await _platformRepo.Delete(existData);
        }

        public async Task Edit(int id, PlatformEditDto model)
        {
            var existData = await _platformRepo.GetById(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            _mapper.Map(model, existData);
            await _platformRepo.Update(existData);
        }

        public async Task<IEnumerable<PlatformDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<PlatformDto>>(await _platformRepo.GetAll());
        }

        public async Task<PlatformDto> GetById(int id)
        {
            var existData = await _platformRepo.GetById(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            return _mapper.Map<PlatformDto>(existData);
        }

        public async Task<Platform> GetByIdRaw(int id)
        {
            var existData = await _platformRepo.GetById(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            return existData;
        }
    }
}
