using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Library
{
    public class LibraryPaginateDto
    {
        public List<Domain.Entities.Library> Products { get; set; }
        public int DataCount { get; set; }
    }
}
