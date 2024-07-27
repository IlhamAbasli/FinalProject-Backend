using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.News
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string NewsContent1 { get; set; }
        public string NewsContent2 { get; set; }
        public string NewsContent3 { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
