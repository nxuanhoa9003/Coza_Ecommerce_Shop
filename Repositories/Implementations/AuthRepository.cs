using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterWithGoogleAsync(RegisterWithGoogleViewModel model)
        {
            var UserNameExist = await _userManager.FindByNameAsync(model.Username);
            if(UserNameExist != null)
            {
                return (false, "Username đã tồn tại");
            }
            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");
            if (userPrincipal == null) return (false, "Chưa đăng nhập");
               
            var claims = userPrincipal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // Lấy Google ProviderKey từ Claims

            // Kiểm tra email hợp lệ
            if (email != model.Email)
            {
                return (false, "Email đăng ký hợp lệ");
            }

            // Kiểm tra xem ProviderKey có hợp lệ không
            if (googleId != model.ProviderKey)
            {
                return (false, "Thông tin Google không hợp lệ, vui lòng thử lại");
            }


            var user = new AppUser
            {
                FullName = model.FullName,
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return (false, "Không thể tạo tài khoản");
            }


            var resultRole = await _userManager.AddToRoleAsync(user, "Customer");

            if (!resultRole.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return (false, "Không thể tạo tài khoản");
            }

            var newClaim = new Claim("UserAccess", "AccessCustomerPage");
            var resultClaim = await _userManager.AddClaimAsync(user, newClaim);

            if (!resultClaim.Succeeded)
            {
                //Xóa user ngay lập tức nếu claim lỗi
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.DeleteAsync(user);
                return (false, "Không thể tạo tài khoản");
            }

            // Liên kết tài khoản Google
            var info = new UserLoginInfo("Google", model.ProviderKey, "Google");
            var resutllogin = await _userManager.AddLoginAsync(user, info);
            if (!resutllogin.Succeeded)
            {
                await _userManager.RemoveClaimAsync(user, newClaim);
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.DeleteAsync(user);
                return (false, "Đăng nhập thất bại, hãy thử lại");
            }


            await LoginForCustomer(user); 

            return (true, "Đăng nhập thành không với Google");
        }


        public async Task LoginForCustomer (AppUser user)
        {
            var listclaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("AvatarUrl", user.AvatarUrl ?? "/Uploads/avt_empty.jpg"),
                    new Claim("UserType", "Customer"),
                };

            var claimsIdentity = new ClaimsIdentity(listclaims, "CustomerScheme");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync("CustomerScheme", claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = true });
        }

        public async Task<UserLoginInfo?> CheckGoogleLogin(AppUser user, string LoginProvider)
        {
            if (string.IsNullOrEmpty(LoginProvider)) return null;
            var hasGoogleLogin = await _userManager.GetLoginsAsync(user);
            return hasGoogleLogin.FirstOrDefault(l => l.LoginProvider == "Google");
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> LinkGoogleAccountAsync(ConfirmLinkGoogleViewModel model)
        {
            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");
            if (userPrincipal == null) return (false, "Chưa đăng nhập");

            var claims = userPrincipal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // Lấy Google ProviderKey từ Claims

            // Kiểm tra email hợp lệ
            if (email != model.Email)
            {
                return (false, "Email đăng ký hợp lệ");
            }

            // Kiểm tra xem ProviderKey có hợp lệ không
            if (googleId != model.ProviderKey)
            {
                return (false, "Thông tin Google không hợp lệ, vui lòng thử lại");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, "Không tìm thấy tài khoản");

            // Kiểm tra xem Google đã liên kết chưa
            var existingLogin = await _userManager.GetLoginsAsync(user);
            if (existingLogin.Any(l => l.LoginProvider == "Google"))
                return (false, "Đã liên kết mail với tài khoản"); // Đã liên kết rồi
                // Thêm thông tin đăng nhập từ Google
            var result = await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", model.ProviderKey, "Google"));
            if (result.Succeeded)
            {
                await LoginForCustomer(user);
                return (true, "Liên kết tài khoản thành công");

            }

            return (true, "Liên kết tài khoản thất bại");
        }
    }
}
