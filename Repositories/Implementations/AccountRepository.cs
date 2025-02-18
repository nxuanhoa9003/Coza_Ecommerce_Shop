﻿using Azure;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountRepository(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> CheckRoleUser(AppUser? user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<AppUser?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<AppUser?> FindByIdAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage, AppUser? user)> LoginAsync(LoginViewModel model, bool isAdmin = false)
        {
            var user = await _userManager.FindByNameAsync(model.UserNameOrEmail)
              ?? await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            if (user == null)
            {
                return (false, "Tài khoản không tồn tại.", null);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                return (false, "Sai mật khẩu.", null);
            }


            // Lấy danh sách role của user
            var roles = await _userManager.GetRolesAsync(user);

            // Xác định loại quyền cần kiểm tra
            string requiredPermission = isAdmin ? "AccessAdminPage" : "AccessCustomerPage";

            if (!await HasPermissionAsync(roles, requiredPermission))
            {
                return (false, $"Bạn không có quyền truy cập vào {(isAdmin ? "trang quản trị" : "trang khách hàng")}.", null);
            }


            if (!user.EmailConfirmed)
                return (false, "Tài khoản chưa xác thực, vui lòng kiểm tra email.", user);


            // Đăng nhập bằng UserName
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);


            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("AvatarUrl", user.AvatarUrl ?? "/Uploads/avt_empty.jpg"),
                    new Claim("UserType", isAdmin ? "Admin" : "Customer")
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authScheme = isAdmin ? "AdminScheme" : "CustomerScheme";

                var claimsIdentity = new ClaimsIdentity(claims, authScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _httpContextAccessor.HttpContext.SignInAsync(authScheme, claimsPrincipal,
                        new AuthenticationProperties { IsPersistent = true });

                return (true, "Đăng nhập thành công!", null);
            }

            return result.IsLockedOut
                    ? (false, "Tài khoản đã bị khóa. Vui lòng thử lại sau.", null)
                    : (false, "Tài khoản hoặc mật khẩu không đúng.", null);
        }


        private async Task<bool> HasPermissionAsync(IEnumerable<string> roles, string requiredPermission)
        {
            var roleList = await _roleManager.Roles
                        .Where(r => roles.Contains(r.Name))
                        .ToListAsync();

            // Kiểm tra quyền trong mỗi role
            foreach (var role in roleList)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                if (roleClaims.Any(c => c.Type == "Permission" && c.Value == requiredPermission))
                {
                    return true; // Nếu có quyền cần thiết, trả về true
                }
            }

            // Nếu không tìm thấy quyền trong bất kỳ role nào
            return false;
        }



        public async Task LogoutAsync(string TypeCookieAccount)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var typecookie = TypeCookieAccount.ToLower().Equals("admin")
                || TypeCookieAccount.ToLower().Equals("employee") ? "AdminScheme" : "CustomerScheme";
            await httpContext.SignOutAsync(typecookie);
            httpContext.Response.Cookies.Delete($".AspNetCore.{typecookie}");
            await _signInManager.SignOutAsync();
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Tạo token xác nhận email
            /*var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{requestUrl}/Identity/Account/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            // Gửi email xác nhận
            await _emailSender.SendEmailAsync(user.Email, "Xác nhận tài khoản",
                $"Vui lòng nhấp vào liên kết để xác nhận tài khoản: <a href='{confirmationLink}'>Xác nhận Email</a>");
            */
            return (true, "Đăng ký tài khoản thành công!");
        }


        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<IEnumerable<AppUser>> GetAllEmployeesAsync(string typeUser = "All")
        {
            IEnumerable<AppUser> users = await _userManager.Users.ToListAsync();
            if (typeUser != "All")
            {

                if (typeUser == "Admin")
                {
                    users = await _userManager.GetUsersInRoleAsync("Admin");
                }
                else if (typeUser == "Employee")
                {
                    users = await _userManager.GetUsersInRoleAsync("Employee");

                }
            }
            return users;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAccountEmployee(EmployeeViewModel employeeViewModel)
        {
            var userExist = await _userManager.FindByNameAsync(employeeViewModel.UserName.Trim());
            var userEmailExist = await _userManager.FindByEmailAsync(employeeViewModel.Email);
            if (userExist != null)
            {
                return (false, "Username đã tồn tại.");
            }
            if (userEmailExist != null)
            {
                return (false, "Email đã tồn tại.");
            }

            var user = new AppUser
            {
                UserName = employeeViewModel.UserName,
                Email = employeeViewModel.Email,
                FullName = employeeViewModel.FullName,
                PhoneNumber = employeeViewModel.PhoneNumber,
                Address = employeeViewModel.Address,
                BirthDate = employeeViewModel.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, employeeViewModel.Password);

            if (!result.Succeeded)
            {
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var role = await _roleManager.FindByIdAsync(employeeViewModel.Role);
            if (role != null)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!roleResult.Succeeded)
                {
                    string errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return (false, $"Tài khoản được tạo nhưng không thể gán vai trò: {errors}");
                }
            }
            else
            {
                return (false, $"Tài khoản được tạo nhưng không thể gán vai trò đã chọn");
            }
            return (true, "Tạo tài khoản nhân viên thành công!");

        }

        public async Task<EmployeeViewModel?> GetEmployeebyId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;
            var role = await _userManager.GetRolesAsync(user);
            var employee = new EmployeeViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DateOfBirth = user.BirthDate.Value,
                Role = string.Join(",", role),
                IsLock = (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow)
            };

            return employee;

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAccountEmployee(EmployeeViewModel employeeViewModel)
        {
            var user = await _userManager.FindByIdAsync(employeeViewModel.Id);
            if (user == null)
            {
                return (false, "Tài khoản không tồn tại.");
            }

            if (user.UserName.ToLower() == "admin")
            {
                return (false, "Không thể cập nhật tài khoản Admin.");
            }

            var userNameExist = await _userManager.FindByNameAsync(employeeViewModel.UserName);
            if (userNameExist != null && userNameExist.Id != employeeViewModel.Id)
            {
                return (false, "Username đã tồn tại.");
            }

            var userEmailExist = await _userManager.FindByEmailAsync(employeeViewModel.Email);
            if (userEmailExist != null && userEmailExist.Email != employeeViewModel.Email)
            {
                return (false, "Email đã được đăng ký trong tài khoản khác.");
            }

            user.UserName = employeeViewModel.UserName;
            if (user.Email != employeeViewModel.Email)
            {
                user.Email = employeeViewModel.Email;
                user.EmailConfirmed = false;
            }
            user.FullName = employeeViewModel.FullName;
            user.PhoneNumber = employeeViewModel.PhoneNumber;
            user.Address = employeeViewModel.Address;
            user.BirthDate = employeeViewModel.DateOfBirth;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return (false, string.Join(", ", updateResult.Errors.Select(e => e.Description)));
            }

            if (!string.IsNullOrWhiteSpace(employeeViewModel.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, employeeViewModel.Password);

                if (!passwordResult.Succeeded)
                {
                    return (false, string.Join(", ", passwordResult.Errors.Select(e => e.Description)));
                }
            }

            var roleOld = await _userManager.GetRolesAsync(user);
            if (!string.IsNullOrEmpty(employeeViewModel.Role) && !roleOld.Contains(employeeViewModel.Role))
            {

                var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, roleOld);
                if (!removeRoleResult.Succeeded)
                {
                    string errors = string.Join(", ", removeRoleResult.Errors.Select(e => e.Description));
                    return (false, $"Không thể xóa vai trò cũ: {errors}");
                }

                var role = await _roleManager.FindByIdAsync(employeeViewModel.Role);
                if (role != null)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!roleResult.Succeeded)
                    {

                        string errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                        return (false, $"Tài khoản được tạo nhưng không thể gán vai trò: {errors}");
                    }
                }
                else
                {
                    return (false, $"Tài khoản được tạo nhưng không thể gán vai trò đã chọn");
                }
            }


            return (true, "Cập nhật tài khoản nhân viên thành công!");
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAccountEmployee(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return (false, "Tài khoản không tồn tại.");
            }

            if (user.UserName == "admin")
            {
                return (false, "Không thể xóa tài khoản Admin.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!removeRolesResult.Succeeded)
                {
                    string errors = string.Join(", ", removeRolesResult.Errors.Select(e => e.Description));
                    return (false, $"Không thể xóa vai trò: {errors}");
                }
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, $"Không thể xóa tài khoản: {errors}");
            }

            return (true, "Xóa tài khoản thành công!");
        }


        // Tạo mã xác thực qua email
        public async Task<string> GenerateEmailVerificationTokenAsync(AppUser user)
        {
            return await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        }

        // Xác thực mã OTP từ email
        public async Task<bool> VerifyEmailVerificationTokenAsync(AppUser user, string token)
        {
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", token);
            if (!isValid) return false;

            // Cập nhật EmailConfirmed thành true
            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<(bool IsSuccess, string ErrorMessage, AppUser? user)> ForgotPasswordAsync(string email, bool IsAdmin = false)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "Email không tồn tại.", null);
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (IsAdmin)
            {
                if (roles.Count == 0 || (!roles.Contains("Admin") && !roles.Contains("Employee")))
                {
                    return (false, "Email không thể truy cập trang Admin.", null);
                }
            }
            else
            {
                if (roles.Contains("Admin") || roles.Contains("Employee"))
                {
                    return (false, "Email không thể truy cập trang Khách hàng.", null);
                }
            }
            // Kiểm tra nếu yêu cầu gần đây (trong vòng 10 phút)
            if (user.LastPasswordResetRequest.HasValue &&
                (DateTime.UtcNow - user.LastPasswordResetRequest.Value).TotalMinutes < 10)
            {
                return (false, "Vui lòng chờ 10 phút trước khi gửi yêu cầu mới.", null);
            }
            return (true, "", user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task UpdateLastPasswordResetRequest(AppUser user)
        {
            user.LastPasswordResetRequest = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return (false, $"Tài khoản có mail {model.Email} không tồn tại");
            }

            // Kiểm tra nếu tài khoản đang bị khóa đặt lại mật khẩu
            if (user.PasswordResetLockoutEnd.HasValue && user.PasswordResetLockoutEnd > DateTime.UtcNow)
            {
                return (false, "Bạn đã nhập sai quá nhiều lần. Vui lòng thử lại sau 5 phút");
            }

            // Thực hiện đặt lại mật khẩu
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                user.FailedPasswordResetAttempts++;
                // Nếu nhập sai 5 lần -> Khóa 5 phút
                if (user.FailedPasswordResetAttempts >= 5)
                {
                    user.PasswordResetLockoutEnd = DateTime.UtcNow.AddMinutes(5);
                    user.FailedPasswordResetAttempts = 0;
                }
                await _userManager.UpdateAsync(user);
                return (false, "Token đặt lại mật khẩu không hợp lệ.");
            }

            // Reset thất bại và cập nhật mật khẩu thành công
            user.FailedPasswordResetAttempts = 0;
            user.PasswordResetLockoutEnd = null;
            await _userManager.UpdateAsync(user);
            return (true, "Mật khẩu đã được đặt lại thành công.");
        }

        public async Task<(bool IsSuccess, string ErrorMessage, bool IsLogout)> UpdateProfile(string emailOld, ProfileViewModel model, bool IsPageAdmin = false)
        {
            bool checkLogOut = false;
            if (string.IsNullOrEmpty(emailOld))
            {
                return (false, "Cập nhật không thành công", checkLogOut);
            }

            var user = await _userManager.FindByEmailAsync(emailOld);
            if (user == null)
            {
                return (false, "Tài khoản muốn cập nhật không tồn tại", checkLogOut);
            }

            if (model.Email != emailOld)
            {

                var mailexist = await _userManager.FindByEmailAsync(model.Email);
                if (mailexist != null)
                {
                    return (false, $"Mail {model.Email} đã tồn tại ở tài khoản khác", checkLogOut);
                }

                checkLogOut = true;
                user.Email = model.Email;
                user.EmailConfirmed = false;
            }

            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.BirthDate = model.DateOfBirth;
            
            if (model.fileImage != null && model.fileImage.Length > 0)
            {
                string folder = IsPageAdmin ? "ProfileAdmin" : "ProfileCustomer";

                if (!string.IsNullOrEmpty(user.AvatarUrl))
                {
                    Utilities.DeleteImage(user.AvatarUrl);
                }

                user.AvatarUrl = await Utilities.UploadFileAsync(model.fileImage, "ProfileAdmin");
                
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return (false, "Cập nhật thông tin thất bại", checkLogOut);
            }



            // update ClaimsPrincipal 
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            var claims = identity.Claims.ToList();

            // Xóa các claims cũ
            claims.RemoveAll(c => c.Type == "FullName" || c.Type == ClaimTypes.Email || c.Type == "AvatarUrl");

            // Thêm claims mới
            claims.Add(new Claim("FullName", user.FullName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("AvatarUrl", user.AvatarUrl ?? "/Uploads/avt_empty.jpg"));

            // Lấy scheme đăng nhập hiện tại
            var authScheme = _httpContextAccessor.HttpContext.User.Identity.AuthenticationType;

            // Tạo ClaimsIdentity mới
            var claimsIdentity = new ClaimsIdentity(claims, authScheme);

            // Tạo ClaimsPrincipal mới
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Cập nhật lại ClaimsPrincipal mà không đăng xuất user
            await _httpContextAccessor.HttpContext.SignInAsync(authScheme, claimsPrincipal,
                new AuthenticationProperties { IsPersistent = true });

            return (true, "Cập nhập thông tin thành công", checkLogOut);

        }


        public async Task<(bool IsSuccess, string ErrorMessage)> ChangePassword(AppUser user, ProfilePasswordViewModel model)
        {
            if (user == null)
            {
                return (false, "Không tìm thấy tài khoản");
            }

            var checkOldPassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (!checkOldPassword)
            {
                return (false, "Mật khẩu cũ không đúng");
            }

            if (model.OldPassword == model.NewPassword)
            {
                return (false, "Mật khẩu mới không được trùng với mật khẩu cũ");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return (true, "Cập nhập mật khẩu thành công");
        }

        public async Task<IEnumerable<ClaimsCustom>> GetAllClaims()
        {
            return await _context.Claims.ToListAsync();
        }


        public async Task<IList<string>> GetRolesByUserAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<IList<Claim>> GetClaimsByUserRolesAsync(AppUser user)
        {
            // Lấy danh sách các roles của user
            var roles = await _userManager.GetRolesAsync(user);
            // Danh sách chứa các claims của mỗi role
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity != null)
                {
                    var claims = await _roleManager.GetClaimsAsync(roleEntity);
                    roleClaims.AddRange(claims); // Thêm các claims của role vào danh sách
                }
            }
            return roleClaims;
        }


       
    }
}
