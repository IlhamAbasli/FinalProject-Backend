using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.DTOs
{
    public class LibraryPaginateDto
    {
        public List<Library> Products { get; set; }
        public int DataCount { get; set; }
    }
}
