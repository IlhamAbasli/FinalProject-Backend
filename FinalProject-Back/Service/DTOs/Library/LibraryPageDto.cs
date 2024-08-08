using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Library
{
    public class LibraryPageDto
    {
        public List<Domain.Entities.Library> LibraryProducts {  get; set; }
        public int PageCount { get; set; }
    }
}
