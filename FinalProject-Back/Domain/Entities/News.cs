using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class News : BaseEntity
    {
        public string NewsTitle { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string NewsContent1 { get; set; }
        public string NewsContent2 { get; set; }
        public string NewsContent3 { get; set; }
        public ICollection<NewsImage> NewsImages { get; set; }

    }
}
