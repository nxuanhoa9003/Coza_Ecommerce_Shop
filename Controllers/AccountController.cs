using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Extentions;
using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IMailService _mailService;
        private readonly EmailTemplateService _templateService;

        private readonly UserService _userService;
        private readonly IAuthRepository _authRepository;

        public INotyfService _notifyService { get; }

        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository, IMailService mailService, EmailTemplateService templateService, UserService userService, IAuthRepository authRepository, INotyfService notifyService)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _templateService = templateService;
            _userService = userService;
            _authRepository = authRepository;
            _notifyService = notifyService;
        }

        [HttpGet("")]
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (await _userService.IsUserTypeAsync("Customer"))
            {
                _notifyService.Warning("Đã có tài khoản khách hàng đăng nhập!");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (await _userService.IsUserTypeAsync("Customer"))
            {
                _notifyService.Warning("Đã có tài khoản khách hàng đăng nhập!");
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage, user) = await _accountRepository.LoginAsync(model, isAdmin: false);
                if (isSuccess)
                {
                    _notifyService.Success(errorMessage);
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
            return View();
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult LoginWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", "Account", null, Request.Scheme)
            };
            return Challenge(properties, "GoogleScheme");
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult RegisterWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", "Account", null, Request.Scheme)
            };
            return Challenge(properties, "GoogleScheme");
        }


        [HttpGet("dang-nhap-tu-google")]
        public async Task<IActionResult> GoogleResponse()
        {
            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");

            if (userPrincipal == null)
                return RedirectToAction("Login");

            var claims = userPrincipal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // ProviderKey từ Google

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            //email chưa tồn tại
            var user = await _accountRepository.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("EnterUsername", new { email = email, providerKey = googleId });
            }

            // Kiểm tra tài khoản bị khóa
            if (await _accountRepository.CheckIsLockedOutAsync(user))
            {
                return BadRequest($"Tài khoản của bạn đã bị khóa đến {user.LockoutEnd}. Vui lòng đăng nhập sau.");
            }

           
            // Kiểm tra tài khoản chưa xác thực email
            if (!user.EmailConfirmed)
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



            var googleLogin = await _authRepository.CheckGoogleLogin(user, "Google");
            if (googleLogin == null)
            {
                // Email đã tồn tại nhưng chưa liên kết Google
                return RedirectToAction("ConfirmLinkGoogle","Account", new { email = email, providerKey = googleId });
            }else if (googleLogin.ProviderKey != googleId)
            {
                // Email đã tồn tại nhưng đã liên kết với tài khoản Google khác
                return BadRequest("Tài khoản Google này không khớp với tài khoản đã liên kết trước đó.");
            }

            //Email đã tồn tại và đã liên kết Google
            await _authRepository.LoginForCustomer(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("[action]")]
        public IActionResult EnterUsername(string email, string providerKey)
        {
            var model = new RegisterWithGoogleViewModel
            {
                Email = email,
                ProviderKey = providerKey
            };
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterWithGoogle(RegisterWithGoogleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EnterUsername", model);
            }
            var (isSuccess, errorMessage) = await _authRepository.RegisterWithGoogleAsync(model);
            if(isSuccess)
            {
                _notifyService.Success(errorMessage);
                return RedirectToAction("Index", "Home");
            }
            _notifyService.Error(errorMessage);
            return RedirectToAction("Login", "Account");
        }


        [HttpGet("[action]")]
        public IActionResult ConfirmLinkGoogle(string email, string providerKey)
        {
            var model = new ConfirmLinkGoogleViewModel
            {
                Email = email,
                ProviderKey = providerKey
            };
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmLinkGoogle(ConfirmLinkGoogleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfirmLinkGoogle", model);
            }

            var (isSuccess, errorMessage) = await _authRepository.LinkGoogleAccountAsync(model);

            if (isSuccess)
            {
                _notifyService.Success(errorMessage);
                return RedirectToAction("Index", "Home");
            }
            _notifyService.Error(errorMessage);
            return RedirectToAction("Login", "Account");

        }




        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (await _userService.IsUserTypeAsync("Customer"))
            {
                await _accountRepository.LogoutAsync(isAdmin: false);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            if (await _userService.IsUserTypeAsync("Customer"))
            {
                _notifyService.Warning("Đã có tài khoản khách hàng đăng nhập!");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await _userService.IsUserTypeAsync("Customer"))
            {
                _notifyService.Warning("Đã có tài khoản khách hàng đăng nhập!");
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage, user) = await _accountRepository.RegisterAsync(model);
                if (isSuccess)
                {
                    if (user != null && !user.EmailConfirmed)
                    {
                        var emailSent = await SendVerificationEmailAsync(user);
                        if (emailSent)
                        {
                            _notifyService.Warning("Mã xác thực đã được gửi đến email của bạn.");
                        }
                        else
                        {
                            _notifyService.Error("Gửi email xác thực thất bại. Vui lòng thử lại.");
                        }
                        return RedirectToAction("VerifyAccount", new { email = user.Email });
                    }
                }

                _notifyService.Error(errorMessage);
            }

            return View();
        }


        [HttpGet("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (isSuccess, errorMessage, user) = await _accountRepository.ForgotPasswordAsync(model.Email, IsAdmin: false);
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


        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel { Email = email, Token = token });
        }

        [HttpPost("[action]")]
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


        [HttpPost("[action]")]
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

        [HttpGet("[action]")]
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

        [HttpPost("[action]")]
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


        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Profile()
        {
            if (await _userService.IsUserTypeAsync("Customer"))
            {
                var userPrincipal = await _userService.GetUserByUserTypeAsync("Customer");
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
            return RedirectToAction("Login", "Account");
        }


        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!await _userService.IsUserTypeAsync("Customer"))
            {
                return RedirectToAction("Login", "Account");
            }

            var userPrincipal = await _userService.GetUserByUserTypeAsync("Customer");
            if (userPrincipal == null)
            {
                _notifyService.Error("Đã có lỗi xảy ra");
                return RedirectToAction("Profile");

            }
            var email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            if (ModelState.IsValid)
            {
                var (IsSuccess, ErrorMessage, IsLogout) = await _accountRepository.UpdateProfile(email, model, IsPageAdmin: false);

                if (IsSuccess)
                {
                    if (IsLogout)
                    {
                        _notifyService.Success("Đăng nhập lại để xác thực tài khoản");
                        await _accountRepository.LogoutAsync(isAdmin: false);
                        return RedirectToAction("Login");

                    }
                    _notifyService.Success(ErrorMessage);
                    return RedirectToAction("Profile");

                }
                _notifyService.Error(ErrorMessage);
            }

            return RedirectToAction("Profile");
        }


        [HttpGet("[action]")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ProfilePasswordViewModel model)
        {
            if (!await _userService.IsUserTypeAsync("Customer"))
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
