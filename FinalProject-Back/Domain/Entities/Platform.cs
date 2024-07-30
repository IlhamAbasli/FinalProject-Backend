using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Platform : BaseEntity
    {
        public string PlatformName { get; set; }
        public string PlatformLogo { get; set; }
        public ICollection<PlatformProducts> PlatformProducts { get; set; }
        public ICollection<PlatformSystemRequirement> PlatformSystemRequirements { get; set;}
    }
}
