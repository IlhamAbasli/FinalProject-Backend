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
    public class PlatformSystemRequirementService : IPlatformSystemRequirementService
    {
        private readonly IPlatformSystemRequirementRepository _platformSystemRepo;
        public PlatformSystemRequirementService(IPlatformSystemRequirementRepository platformSystemRepo)
        {
            _platformSystemRepo = platformSystemRepo;
        }
        public async Task Create(PlatformSystemRequirement model)
        {
            await _platformSystemRepo.Create(model);
        }
    }
}
