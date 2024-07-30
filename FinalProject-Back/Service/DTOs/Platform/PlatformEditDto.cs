using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Platform
{
    public class PlatformEditDto
    {
        public string PlatformName { get; set; }
        public string PlatformLogo {  get; set; }
        public IFormFile NewPlatformLogo { get; set; }
    }
}
