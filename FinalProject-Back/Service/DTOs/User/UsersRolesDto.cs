using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.User
{
    public class UsersRolesDto
    {
        public List<UsersDto> Users { get; set; }
        public List<RolesDto> Roles { get; set; }
    }
}
