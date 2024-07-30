using AutoMapper;
using Domain.Entities;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs;
using Service.DTOs.Ad;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepo;
        private readonly IMapper _mapper;
        public AdService(IAdRepository adRepo,IMapper mapper)
        {
            _adRepo = adRepo;
            _mapper = mapper;
        }
        public async Task Create(Advertisement model)
        {
            await _adRepo.Create(model);
        }

        public async Task Delete(int id)
        {
            var existAd = await _adRepo.GetById(id);
            if (existAd is null) throw new NotFoundException("Ad not found with this ID");
            await _adRepo.Delete(existAd);
        }

        public async Task Edit(int id, AdEditDto model)
        {
            var existAd = await _adRepo.GetById(id);
            if (existAd is null) throw new NotFoundException("Ad not found with this ID");
            _mapper.Map(model,existAd);
            await _adRepo.Update(existAd);
        }

        public async Task<IEnumerable<AdDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<AdDto>>(await _adRepo.GetAll());
        }

        public async Task<AdDto> GetById(int id)
        {
            return _mapper.Map<AdDto>(await _adRepo.GetById(id));
        }
    }
}
