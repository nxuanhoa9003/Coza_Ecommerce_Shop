using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Extentions;
using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers.Identity
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IMailService _mailService;
        private readonly EmailTemplateService _templateService;
        private readonly UserService _userService;
        public INotyfService _notifyService { get; }

        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository, IMailService mailService, EmailTemplateService templateService, UserService userService, INotyfService notifyService)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _templateService = templateService;
            _userService = userService;
            _notifyService = notifyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (await _userService.IsUserTypeAsync("Admin"))
            {
                return RedirectToAction("", "Home", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Admin");
            ViewData["ReturnUrl"] = returnUrl;

            if (await _userService.IsUserTypeAsync("Admin"))
            {
                return RedirectToAction("", "Home", new { area = "Admin" });
            }

            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage, user) = await _accountRepository.LoginAsync(model, isAdmin: true);
                if (isSuccess)
                {
                    _notifyService.Success(errorMessage);
                    if (string.IsNullOrEmpty(returnUrl) || returnUrl.Contains("/logout", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("", "Home", new { area = "Admin" });
                    }

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    if (user != null && !user.EmailConfirmed)
                    {
                        var emailSent = await SendVerificationEmailAsync(user);
                        if (emailSent)
                        {
                            _notifyService.Warning("Tài khoản chưa xác thực. Mã xác thực đã được gửi đến email của bạn.");
                        }
                        else
                        {
                            _notifyService.Error("Gửi email xác thực thất bại. Vui lòng thử lại.");
                        }
                        return RedirectToAction("VerifyAccount", new { email = user.Email });
                    }

                    _notifyService.Error(errorMessage);
                }
            }
            return View(model);
        }

        [HttpPost("/logout/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (await _userService.IsUserTypeAsync("Admin"))
            {
                await _accountRepository.LogoutAsync(isAdmin: true);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (isSuccess, errorMessage, user) = await _accountRepository.ForgotPasswordAsync(model.Email, IsAdmin: true);
            if (isSuccess)
            {
                var token = await _accountRepository.GeneratePasswordResetTokenAsync(user);

                // Tạo link reset password
                var resetLink = Url.Action("ResetPassword", "Account",
                    new { email = model.Email, token = token }, Request.Scheme);

                // Đọc template HTML và thay thế thông tin
                var placeholders = new Dictionary<string, string>
                {
                    { "USERNAME", user.UserName },
                    { "RESET_LINK", resetLink }
                };

                var emailBody = await _templateService.GetEmailTemplateAsync("reset_password", placeholders);

                await _mailService.SendEmailAsync(user.Email, "Đặt lại mật khẩu", emailBody);
                await _accountRepository.UpdateLastPasswordResetRequest(user);

                return RedirectToAction("Success");
            }
            else
            {
                _notifyService.Error(errorMessage);
            }
            return RedirectToAction("ForgotPassword");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel { Email = email, Token = token });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _accountRepository.ResetPassword(model);
                if (isSuccess)
                {
                    _notifyService.Success(errorMessage);
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationCode([FromForm] string email, bool IsResend = false)
        {
            if (string.IsNullOrEmpty(email))
            {
                _notifyService.Error("Vui lòng nhập email.");
                return RedirectToAction("Login");
            }
            var user = await _accountRepository.FindByEmailAsync(email);
            if (user == null) return NotFound();

            // Nếu tài khoản đã xác thực, không cần gửi mã nữa
            if (user.EmailConfirmed)
            {
                _notifyService.Warning("Tài khoản này đã được xác thực.");
                return RedirectToAction("Login");
            }
            var emailSent = await SendVerificationEmailAsync(user);
            if (emailSent)
            {
                if (IsResend)
                {
                    _notifyService.Success("Mã xác thực đã được gửi lại.");
                }
                else
                {
                    _notifyService.Warning("Tài khoản chưa xác thực. Mã xác thực đã được gửi đến email của bạn.");
                }
            }
            else
            {
                _notifyService.Error("Gửi email xác thực thất bại. Vui lòng thử lại.");
            }


            return View("VerifyAccount", new VerifyAccountViewModel { Email = email });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyAccount(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _notifyService.Error("Email không hợp lệ.");
                return RedirectToAction("Login");
            }

            var model = new VerifyAccountViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyAccount(VerifyAccountViewModel model)
        {
            var user = await _accountRepository.FindByEmailAsync(model.Email);
            if (user == null) return NotFound();

            var result = await _accountRepository.VerifyEmailVerificationTokenAsync(user, model.Code);
            if (result)
            {
                _notifyService.Success("Xác thực tài khoản thành công. Hãy đăng nhập lại");
                return RedirectToAction("Login");
            }
            _notifyService.Error("Xác thực tài khoản thất bại");
            return View(model);
        }


        private async Task<bool> SendVerificationEmailAsync(AppUser user)
        {
            if (user == null) return false;

            try
            {
                var token = await _accountRepository.GenerateEmailVerificationTokenAsync(user);
                // Đọc template HTML và thay thế thông tin
                var placeholders = new Dictionary<string, string>
                {
                    { "USERNAME", user.UserName },
                    { "CODE", token }
                };
                var emailBody = await _templateService.GetEmailTemplateAsync("verification_email", placeholders);

                await _mailService.SendEmailAsync(user.Email, "Mã xác thực tài khoản", emailBody);

                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                return false;
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Success()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            if (await _userService.IsUserTypeAsync("Admin"))
            {
                var userPrincipal = await _userService.GetUserByUserTypeAsync("Admin");
                if (userPrincipal != null)
                {
                    var email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;

                    var userProfile = await _accountRepository.GetProfileByEmail(email);
                    if (userProfile != null)
                    {
                        return View(userProfile);
                    }

                }
            }

            return RedirectToAction("Login");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!await _userService.IsUserTypeAsync("Admin"))
            {
                return RedirectToAction("Login");
            }

            var userPrincipal = await _userService.GetUserByUserTypeAsync("Admin");
            if (userPrincipal == null)
            {
                _notifyService.Error("Đã có lỗi xảy ra");
                return RedirectToAction("Profile");

            }
            var email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            if (ModelState.IsValid)
            {
                var (IsSuccess, ErrorMessage, IsLogout) = await _accountRepository.UpdateProfile(email, model, true);

                if (IsSuccess)
                {
                    if (IsLogout)
                    {
                        _notifyService.Success("Đăng nhập lại để xác thực tài khoản");
                        await _accountRepository.LogoutAsync(isAdmin: true);
                        return RedirectToAction("Login");

                    }
                    _notifyService.Success(ErrorMessage);
                    return RedirectToAction("Profile");

                }
                _notifyService.Error(ErrorMessage);
            }

            return RedirectToAction("Profile");
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ProfilePasswordViewModel model)
        {
            if (!await _userService.IsUserTypeAsync("Admin"))
            {
                _notifyService.Error("Đăng nhập để cập nhật tài khoản");
                return RedirectToAction("Login");
            }


            if (ModelState.IsValid)
            {
                var email = User.FindFirst(ClaimTypes.Email);
                var user = await _accountRepository.FindByEmailAsync(email.Value.ToString());

                var (IsSuccess, ErrorMessage) = await _accountRepository.ChangePassword(user, model);
                if (IsSuccess)
                {
                    _notifyService.Success(ErrorMessage);
                    return View();

                }
                ModelState.AddModelError("", ErrorMessage);
                _notifyService.Error(ErrorMessage);
            }
            return View();
        }


    }
}
