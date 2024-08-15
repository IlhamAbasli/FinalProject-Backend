using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.User
{
    public class AddRoleToUserDto
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }  
    }
}
