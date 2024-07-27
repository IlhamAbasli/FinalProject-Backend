using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlatformProducts : BaseEntity
    {
        public int ProductId { get; set; }  
        public Product Product { get; set; }
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
