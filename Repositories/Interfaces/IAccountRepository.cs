using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CheckIsLockedOutAsync(AppUser? user);
        Task<bool> CheckRoleUser(AppUser? user, string role);
        Task <AppUser?> FindByEmailAsync(string email);
        Task <AppUser?> FindByIdAsync(string Id);

        Task<IList<string>> GetRolesByUserAsync(AppUser user);
        Task<IList<Claim>> GetClaimsByUserRolesAsync(AppUser user);

        Task<(bool IsSuccess, string ErrorMessage, AppUser? user)> LoginAsync(LoginViewModel model, bool isAdmin = false);
        Task LogoutAsync(bool isAdmin = false);
        Task<(bool IsSuccess, string ErrorMessage, AppUser? user)> RegisterAsync(RegisterViewModel model);
        Task<bool> ConfirmEmailAsync(string userId, string token);

        Task <string> GenerateEmailVerificationTokenAsync(AppUser user);
        Task<bool> VerifyEmailVerificationTokenAsync(AppUser user, string token);

        Task<string> GeneratePasswordResetTokenAsync(AppUser user);

        Task<(bool IsSuccess, string ErrorMessage, AppUser? user)> ForgotPasswordAsync(string email, bool IsAdmin);

        Task UpdateLastPasswordResetRequest(AppUser user);
        Task<(bool IsSuccess, string ErrorMessage)> ResetPassword(ResetPasswordViewModel model);

        Task<EmployeeViewModel?> GetEmployeebyId(string id);

        Task<ProfileViewModel?> GetProfileByEmail(string email);
        Task<(bool IsSuccess, string ErrorMessage, bool IsLogout)> UpdateProfile(string emailOld, ProfileViewModel model, bool IsPageAdmin = false);
        Task<(bool IsSuccess, string ErrorMessage)> ChangePassword(AppUser user, ProfilePasswordViewModel model);



        Task<IEnumerable<AppUser>> GetAllEmployeesAsync(string typeUser);
        Task<(bool IsSuccess, string ErrorMessage)> CreateAccountEmployee(EmployeeViewModel employeeViewModel);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateAccountEmployee(EmployeeViewModel employeeViewModel);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteAccountEmployee(string Id);

        // customers
        Task<IEnumerable<AppUser>> GetAllCustomersAsync(string typeUser);
        Task<CustomerDetailViewModel?> GetDetailCustomersAsync(string Id);

        Task<IEnumerable<ClaimsCustom>> GetAllClaims();
    }
}
