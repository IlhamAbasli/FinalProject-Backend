using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Advertisement : BaseEntity
    {
        public string AdImage { get; set; }
        public string AdTitle { get; set; }
        public string AdDescription { get; set; }
    }
}
