using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.User
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public List<string> ExistRoles { get; set; }
    }
}
