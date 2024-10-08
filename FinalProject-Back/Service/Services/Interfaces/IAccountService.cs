﻿using Service.DTOs.Account;
using Service.Helpers.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResponse> SignUp(RegisterDto model);
        Task<UserRoleResponse> AddRoleToUser(UserRoleDto model);
        Task<LoginResponse> SignIn(LoginDto model);
        Task UpdateUser(string userId, UserUpdateDto model);
        Task ConfirmEmail(string userId, string token);
        Task ResetPassword(ResetPasswordDto model);
        Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordDto model);
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordDto model);
        Task CreateRoles();
    }
}
