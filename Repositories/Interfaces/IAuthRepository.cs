using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IAuthRepository
    {

        Task<(bool IsSuccess, string ErrorMessage)> RegisterWithGoogleAsync(RegisterWithGoogleViewModel model);
        Task<(bool IsSuccess, string ErrorMessage)> LinkGoogleAccountAsync(ConfirmLinkGoogleViewModel model);
        Task<UserLoginInfo?> CheckGoogleLogin(AppUser user, string LoginProvider);
        Task LoginForCustomer(AppUser user);
    }
}
