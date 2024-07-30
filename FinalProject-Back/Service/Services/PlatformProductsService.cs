using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PlatformProductsService : IPlatformProductsService
    {
        private readonly IPlatformProductsRepository _platformProductRepo;
        public PlatformProductsService(IPlatformProductsRepository platformProductRepo)
        {
            _platformProductRepo = platformProductRepo;
        }
        public async Task Create(PlatformProducts model)
        {
            await _platformProductRepo.Create(model);
        }
    }
}
