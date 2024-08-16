using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMail(string to, string subject, string html, string from = null);
    }
}
