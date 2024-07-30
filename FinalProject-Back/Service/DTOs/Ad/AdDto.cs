using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Ad
{
    public class AdDto
    {
        public int Id { get; set; }
        public string AdTitle { get; set; }
        public string AdDescription { get; set; }
        public string AdImage { get; set; }
    }
}
