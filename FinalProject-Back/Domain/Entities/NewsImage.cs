using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NewsImage : BaseEntity
    {
        public string Image {  get; set; }
        public bool IsMain { get; set; } = false;
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
