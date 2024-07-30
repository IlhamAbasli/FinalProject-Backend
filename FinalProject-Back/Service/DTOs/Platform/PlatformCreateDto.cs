using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Platform
{
    public class PlatformCreateDto
    {
        public string PlatformName { get; set; }
        public IFormFile PlatformLogo { get; set; }
    }
}
