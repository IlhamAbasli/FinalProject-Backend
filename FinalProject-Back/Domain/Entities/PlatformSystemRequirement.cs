using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlatformSystemRequirement : BaseEntity
    { 
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        public int SystemRequirementId { get; set; }
        public SystemRequirement SystemRequirement { get; set; }
    }
}
