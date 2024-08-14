using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Type;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepo;
        private readonly IMapper _mapper;
        public TypeService(ITypeRepository typeRepo,IMapper mapper)
        {
            _typeRepo = typeRepo;
            _mapper = mapper;
        }
        public async Task Create(TypeCreateDto model)
        {
            await _typeRepo.Create(_mapper.Map<ProductType>(model));
        }

        public async Task Delete(int id)
        {
            var existType = await _typeRepo.GetById(id);
            if (existType is null) throw new NotFoundException("Type not found with this ID");
            await _typeRepo.Delete(existType);
        }

        public async Task Edit(int id, TypeEditDto model)
        {
            var existType = await _typeRepo.GetById(id);
            if (existType is null) throw new NotFoundException("Type not found with this ID");
            _mapper.Map(model,existType);
            await _typeRepo.Update(existType);  
        }

        public async Task<IEnumerable<TypeDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<TypeDto>>(await _typeRepo.GetAll());
        }

        public async Task<TypeDto> GetById(int id)
        {
            var existData = await _typeRepo.GetById(id);
            if (existData is null) throw new NotFoundException("Data not found with this ID");
            return _mapper.Map<TypeDto>(existData);
        }

        public async Task<bool> TypeIsExist(string typeName)
        {
            return await _typeRepo.TypeIsExist(typeName);
        }
    }
}
