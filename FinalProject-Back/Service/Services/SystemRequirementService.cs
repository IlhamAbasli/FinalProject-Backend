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
    public class SystemRequirementService : ISystemRequirementService
    {
        private readonly ISystemRequirementRepository _systemRequirementRepo;
        public SystemRequirementService(ISystemRequirementRepository systemRequirementRepo)
        {
            _systemRequirementRepo = systemRequirementRepo;
        }
        public async Task Create(SystemRequirement model)
        {
            await _systemRequirementRepo.Create(model);
        }
    }
}
