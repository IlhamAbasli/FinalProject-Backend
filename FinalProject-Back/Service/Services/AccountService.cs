using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.DTOs.Account;
using Service.Helpers.Account;
using Service.Helpers.Enums;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<AppUser> userManager,
                              IMapper mapper,
                              RoleManager<IdentityRole> roleManager,
                              IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task<UserRoleResponse> AddRoleToUser(UserRoleDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                return new UserRoleResponse { Success = false, Message = "Role is already exist in this user" };
            }
            await _userManager.AddToRoleAsync(user, role.Name);
            return new UserRoleResponse { Success = true, Message = "Role added to user" };
        }

        public async Task<RegisterResponse> SignUp(RegisterDto model)
        {
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse { Success = false, Errors = result.Errors.Select(m => m.Description) };
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            return new RegisterResponse { Success = true, Errors = null };
        }

        public async Task<LoginResponse> SignIn(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailOrUsername);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(model.EmailOrUsername);
            }

            if (user is null)
            {
                return new LoginResponse { Success = false, Message = "Login failed" };
            }

            var response = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!response)
            {
                return new LoginResponse { Success = false, Message = "Login failed" };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            string token = GenerateJwtToken(user.UserName, user.Id, user.Email, user.Firstname, user.Lastname, (List<string>)userRoles);

            return new LoginResponse { Success = true, Message = "Login success", Token = token };
        }

        private string GenerateJwtToken(string username, string userId, string userEmail, string firstName, string lastName, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sid, userId),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, username)
        };

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpireDays));

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
