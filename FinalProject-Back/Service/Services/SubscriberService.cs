using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Subscriber;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepo;
        private readonly IMapper _mapper;
        public SubscriberService(ISubscriberRepository subscriberRepo,IMapper mapper)
        {
            _subscriberRepo = subscriberRepo;
            _mapper = mapper;   
        }
        public async Task Create(SubscriberCreateDto model)
        {
            await _subscriberRepo.Create(_mapper.Map<Subscriber>(model));
        }

        public async Task Delete(SubscriberDeleteDto model)
        {
            var existSubscriber = await _subscriberRepo.FindBy(m=>m.User.Id == model.UserId).FirstOrDefaultAsync();
            await _subscriberRepo.Delete(existSubscriber);
        }

        public async Task<SubscriberDto> GetSubscriber(string subscriberId)
        {
            var existSubscriber = await _subscriberRepo.FindBy(m=>m.UserId == subscriberId, m=>m.User).FirstOrDefaultAsync();
            return _mapper.Map<SubscriberDto>(existSubscriber);
        }
    }
}
