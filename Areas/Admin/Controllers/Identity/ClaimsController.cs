using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers.Identity
{

    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class ClaimsController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public ClaimsController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [Authorize(Policy = "ViewPermissions")]
        public async Task<IActionResult> Index()
        {
            var claims = await _roleRepository.GetAllClaimsAsync();

            var claimsGrouped = claims.GroupBy(x => x.TypeClaim).Select(x => new ClaimCustomGroup
            {
                TypeClaim = x.Key,
                Claims = x.ToList()
            }).ToList();
            return View(claimsGrouped);
        }
    }
}
