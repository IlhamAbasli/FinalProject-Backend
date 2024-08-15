using Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task RemoveRole(RemoveRoleDto model);
        Task AddRoleToUser(AddRoleToUserDto model);
        Task<UsersRolesDto> GetUsersRoles();
    }
}
