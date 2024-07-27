using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
