using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers.Identity
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleRepository _roleRepository;

        public INotyfService _notifyService { get; }
        public RoleController(IRoleRepository roleRepository, ILogger<RoleController> logger, INotyfService notifyService)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _notifyService = notifyService;
        }

        [Authorize(Policy = "ViewRoles")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return View(roles);
        }

        [HttpGet]
        [Authorize(Policy = "CreateRoles")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateRoles")]
        public async Task<IActionResult> Create([FromForm] string roleName)
        {
            if(string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "Role name is not empty !");
                return View();
            }

            var rs = await _roleRepository.CreateRoleAsync(roleName);

            if (!rs.IsSuccess)
            {
                _notifyService.Error("Tạo vai trò thất bại !");
                ModelState.AddModelError("", rs.ErrorMessage);
                return View();
            }
            _notifyService.Success("Tạo vai trò thành công !");
            return RedirectToAction("Index");

        }


        [HttpGet]
        [Authorize(Policy = "EditRoles")]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [Authorize(Policy = "EditRoles")]
        public async Task<IActionResult> Edit([FromForm]IdentityRole role)
        {
            var rs = await _roleRepository.UpdateRoleAsync(role);
            if(rs.IsSuccess)
            {
                _notifyService.Success("Cập nhật vai trò thành công !");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", rs.ErrorMessage);
            _notifyService.Error(rs.ErrorMessage);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteRoles")]
        public async Task<IActionResult> Delete(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var rs = await _roleRepository.DeleteRoleAsync(id);
            if (rs.IsSuccess)
            {
                _notifyService.Success("Xóa vai trò thành công !");
            }else
            {
                _notifyService.Error(rs.ErrorMessage);

            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Policy = "UpdateRoleClaims")]
        public async Task<IActionResult> UpdateRoleClaims(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            
            var role = await _roleRepository.GetRoleByIdAsync(id);

            if (role == null)
            {
                _notifyService.Error("Role không tồn tại");
            }

            var allClaims = await _roleRepository.GetAllClaimsAsync();
            var roleClaims = await _roleRepository.GetClaimsAsync(role);

            var claims = allClaims.Select(c => new ClaimCheckBoxItem
            {
                ClaimName = c.Name,
                ClaimType = c.ClaimType,
                ClaimValue = c.ClaimValue,
                IsSelected = roleClaims.Any(rc => rc.Type == c.ClaimType && rc.Value == c.ClaimValue),
                TypeClaim = c.TypeClaim
                
            }).ToList();

            var groupedClaims = claims.GroupBy(x => x.TypeClaim).Select(x => new ClaimGroup
            {
                TypeClaim = x.Key,
                Claims = x.ToList()
            }).ToList();

            var roleClaimVm = new RoleClaimsCustomViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                ClaimGroups = groupedClaims
            };

            return View(roleClaimVm);

        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "UpdateRoleClaims")]
        public async Task<IActionResult> UpdateRoleClaims (string Id, List<string> selectedClaims)
        {
            if(string.IsNullOrEmpty(Id))
            {
                _notifyService.Error("Không tìm thấy role");
            }

            try
            {
                var ( IsSuccess, ErrorMessage) = await _roleRepository.UpdateClaimToRole(Id, selectedClaims);
                if(IsSuccess)
                {
                    _notifyService.Success(ErrorMessage);
                }else
                {
                    _notifyService.Error("Đã có lỗi xảy ra, hãy thử lại");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _notifyService.Error("Không thể cập nhật quyền cho vai trò");
            }

            return RedirectToAction("Index");
        }

    }
}
