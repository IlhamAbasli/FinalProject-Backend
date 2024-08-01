using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Account
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }
}
