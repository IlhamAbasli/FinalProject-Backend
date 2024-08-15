using AutoMapper;
using Domain.Entities;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Text;
using MimeKit;
using Repository.Helpers.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Account;
using Service.Helpers.Account;
using Service.Helpers.Enums;
using Service.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MailKit.Net.Smtp;

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

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url = GenerateEmailConfirmationLink(user.Id, token);
            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/emailconfirmation.html"))
            {
                html = await reader.ReadToEndAsync();
            }

            html = html.Replace("{link}", url);
            html = html.Replace("{Username}", $"{user.Firstname} {user.Lastname}");
            string subject = "Email confirmation";

            SendMail(user.Email, subject, html);


            return new RegisterResponse { Success = true, Errors = null };
        }

        public async Task ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var decodedToken = HttpUtility.UrlDecode(token);
            await _userManager.ConfirmEmailAsync(user, decodedToken);
        }

        public void SendMail(string to, string subject, string html, string from = null)
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ilhamra@code.edu.az"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ilhamra@code.edu.az", "vtiw pogc prau vewp");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        private string GenerateEmailConfirmationLink(string userId, string token)
        {
            var uriBuilder = new UriBuilder("http://localhost:5173/emailconfirmation");
            var query = HttpUtility.ParseQueryString(uriBuilder.ToString());
            query["userId"] = userId;
            query["token"] = HttpUtility.UrlEncode(token);
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        private string GeneratePasswordResetLink(string userId, string token)
        {
            var uriBuilder = new UriBuilder("http://localhost:5173/resetpassword");
            var query = HttpUtility.ParseQueryString(uriBuilder.ToString());
            query["userId"] = userId;
            query["token"] = HttpUtility.UrlEncode(token);
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        public async Task<LoginResponse> SignIn(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailOrUsername);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(model.EmailOrUsername);
            }

            if (user is null || !user.EmailConfirmed)
            {
                return new LoginResponse { Success = false, Message = "Login failed,check your login credential" };
            }

            var response = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!response)
            {
                return new LoginResponse { Success = false, Message = "Login failed,check your login credential" };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            string token = GenerateJwtToken(user.UserName, user.Id, user.Email, user.Firstname, user.Lastname, (List<string>)userRoles);

            return new LoginResponse { Success = true, Message = "Login success, redirecting to home page", Token = token };
        }

        private string GenerateJwtToken(string username, string userId, string userEmail, string firstName, string lastName, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sid, userId),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, username)
        };
            if(firstName is not null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, firstName));
            };
            if(lastName is not null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, lastName));
            }

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

        public async Task UpdateUser(string userId, UserUpdateDto model)
        {
            var existUsername = await _userManager.FindByNameAsync(model.Username);
            var user = await _userManager.FindByIdAsync(userId);
            if (existUsername is not null && user.UserName != existUsername.UserName)
            {
                throw new BadRequestException("This username has already exist");
            }
            user.UserName = model.Username;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            await _userManager.UpdateAsync(user);
        }

        public async Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordDto model)
        {
            AppUser existUser = await _userManager.FindByEmailAsync(model.Email);
            if (existUser is null)
            {
                return new ForgetPasswordResponse { Success = false, Message = "Sorry, your account was not found" };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string url = GeneratePasswordResetLink(existUser.Id, token);

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/passwordresetconfirm.html"))
            {
                html = await reader.ReadToEndAsync();
            }

            html = html.Replace("{link}", url);
            html = html.Replace("{Username}", $"{existUser.Firstname} {existUser.Lastname}");
            string subject = "Verify to reset your password";

            SendMail(existUser.Email, subject, html);

            return new ForgetPasswordResponse { Success = true, Message = "Check your email" };
        }

        public async Task ResetPassword(ResetPasswordDto model)
        {
            var existUser = await _userManager.FindByIdAsync(model.UserId);

            if (existUser is null)
            {
                throw new NotFoundException("User not found");
            }
            var decodedToken = HttpUtility.UrlDecode(model.Token);

            await _userManager.ResetPasswordAsync(existUser, decodedToken, model.NewPassword);
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordDto model)
        {
            var existUser = await _userManager.FindByIdAsync(model.UserId);
            var existPassword = await _userManager.CheckPasswordAsync(existUser, model.NewPassword);
            var checkOldPassword = await _userManager.CheckPasswordAsync(existUser,model.OldPassword);

            if (!checkOldPassword)
            {
                return new ChangePasswordResponse { Success = false, Message = "Old password is not correct" };

            }

            if (existPassword)
            {
                return new ChangePasswordResponse { Success = false, Message = "New password can`t be the same with old password" };

            }
            await _userManager.ChangePasswordAsync(existUser,model.OldPassword,model.NewPassword);
            return new ChangePasswordResponse { Success = true, Message = "Password changed successfully" };
        }
    }
}
