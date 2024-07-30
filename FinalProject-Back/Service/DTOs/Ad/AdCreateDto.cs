using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Ad
{
    public class AdCreateDto
    {
        public string AdTitle { get; set; }
        public string AdDescription { get; set; }
        public IFormFile AdImage { get; set; }
    }
}
