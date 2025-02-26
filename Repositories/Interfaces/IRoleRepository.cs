using Coza_Ecommerce_Shop.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<List<IdentityRole>> GetAllRolesAdminPageAsync();
        Task<IdentityRole?> GetRoleByIdAsync(string id);
        Task<(bool IsSuccess, string ErrorMessage)> CreateRoleAsync(string roleName);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateRoleAsync(IdentityRole role);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(string roleId);

        Task<IEnumerable<ClaimsCustom>> GetAllClaimsAsync();

        Task<IEnumerable<Claim>> GetClaimsAsync(IdentityRole role);

        Task<(bool IsSuccess, string ErrorMessage)> UpdateClaimToRole(string roleId, List<string> selectedClaims);

    }
}
