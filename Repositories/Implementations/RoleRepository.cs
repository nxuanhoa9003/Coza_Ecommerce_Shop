using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateRoleAsync(string roleName)
        {
            var roleExist = await _roleManager.FindByNameAsync(roleName);
            if (roleExist != null)
            {
                return (false, "Role already exists");
            }



            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                return (false, string.Join(",", result.Errors.Select(x => x.Description)));
            }
            return (result.Succeeded, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return (false, "Not found name");
            }

            var protectedRoles = new List<string> { "admin", "customer", "employee" };
            if (protectedRoles.Contains(role.Name.ToLower()))
                return (false, $"Role: {role.Name} là mặc định, không thể xoá!");

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded
                ? (true, string.Empty)
                : (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }



        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var orderedRoles = roles
                .OrderBy(r => r.Name.ToLower() switch
                {
                    "admin" => 0,
                    "customer" => 1,
                    "employee" => 2,
                    _ => 3
                })
                .ThenBy(r => r.Name)
                .ToList();

            return orderedRoles;
        }

        public async Task<IdentityRole?> GetRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateRoleAsync(IdentityRole role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id);
            if (existingRole == null) return (false, "Not found name");

            var roleNameExist = await _roleManager.FindByNameAsync(role.Name);
            if (roleNameExist != null && roleNameExist.Id != role.Id)
                return (false, "Tên Role đã tồn tại.");

            existingRole.Name = role.Name;
            var result = await _roleManager.UpdateAsync(existingRole);

            return result.Succeeded
                ? (true, string.Empty)
                : (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<IEnumerable<ClaimsCustom>> GetAllClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(IdentityRole role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateClaimToRole(string roleId, List<string> selectedClaims)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return (false, "Không tìm thấy role đã chọn");
            }


            // Lấy danh sách quyền hiện tại của role
            var currentClaims = await _roleManager.GetClaimsAsync(role);
            // Chuyển đổi currentClaims thành danh sách các ClaimValue (string)
            var currentClaimValues = currentClaims.Select(c => c.Value).ToList();

            // Tìm các quyền theo ID trong table claims
            var claimsFromDb = await _context.Claims
            .Where(c => selectedClaims.Contains(c.ClaimValue.ToString()))
            .ToListAsync();

            var selectedClaimValues = claimsFromDb.Select(c => c.ClaimValue).ToHashSet();

            // Xóa quyền bị bỏ chọn 
            var claimsToRemove = currentClaims
            .Where(c => !selectedClaimValues.Contains(c.Value))
            .ToList();

            foreach (var claim in claimsToRemove)
            {
                var rsremove = await _roleManager.RemoveClaimAsync(role, claim);
                if (!rsremove.Succeeded)
                {
                    return (false, "Xoá quyền thất bại");

                }
            }

            // Thêm quyền mới nếu chưa có
            var claimsToAdd = claimsFromDb
                .Where(c => !currentClaimValues.Contains(c.ClaimValue))
                .ToList();


            foreach (var claim in claimsToAdd)
            {
                var rsradd = await _roleManager.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));
                if (!rsradd.Succeeded)
                {
                    return (false, "Thêm quyền thất bại");

                }
            }

            return (true, "Cập nhật quyền thành công");

        }
    }
}
