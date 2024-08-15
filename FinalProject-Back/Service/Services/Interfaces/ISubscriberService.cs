using Domain.Entities;
using Service.DTOs.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task Create(SubscriberCreateDto model);
        Task Delete(SubscriberDeleteDto model);
        Task<SubscriberDto> GetSubscriber(string subscriberId);
    }
}
