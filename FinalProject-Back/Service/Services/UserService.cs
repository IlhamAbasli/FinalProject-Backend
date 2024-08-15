using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers.Exceptions;
using Service.DTOs.User;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager; 
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            List<UserDto> userRoles = new();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                string rolesStr = string.Join(",", roles);
                userRoles.Add(new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    Roles = rolesStr,
                    ExistRoles = roles.ToList()
                });
            }

            return userRoles;
        }

        public async Task RemoveRole(RemoveRoleDto model)
        {
            var existUser = await _userManager.FindByIdAsync(model.UserId);
            await _userManager.RemoveFromRoleAsync(existUser,model.Role);
        }

        public async Task AddRoleToUser(AddRoleToUserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if(await _userManager.IsInRoleAsync(user, role.Name))
            {
                throw new BadRequestException("Role has already exist in this user");
            }

            await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<UsersRolesDto> GetUsersRoles()
        {
            var users = _mapper.Map<List<UsersDto>>(await _userManager.Users.ToListAsync());
            var roles = _mapper.Map<List<RolesDto>>(await _roleManager.Roles.ToListAsync());

            return new UsersRolesDto { Users = users, Roles = roles };
        }
    }
}
