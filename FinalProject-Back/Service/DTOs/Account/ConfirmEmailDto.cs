using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Account
{
    public class ConfirmEmailDto
    {
        public string userId {  get; set; } 
        public string token { get; set; }
    }
}
