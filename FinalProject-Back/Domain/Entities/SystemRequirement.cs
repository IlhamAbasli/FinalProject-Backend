using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SystemRequirement : BaseEntity
    {
        public string MinOsVersion { get; set; }
        public string MinCpuName { get; set; }
        public string MinMemory {  get; set; }
        public string MinGpu { get; set; }
        public string RecomOsVersion { get; set; }
        public string RecomCpuName { get; set; }
        public string RecomMemory { get; set; }
        public string RecomGpu { get; set; }
        public ICollection<PlatformSystemRequirement> PlatformSystemRequirements { get; set; }
    }
}
