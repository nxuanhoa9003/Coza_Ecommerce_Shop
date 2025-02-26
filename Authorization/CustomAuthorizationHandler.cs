using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Authorization
{
    public class CustomAuthorizationHandler : IAuthorizationHandler
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<CustomAuthorizationHandler> _logger;

        public CustomAuthorizationHandler(IAccountRepository accountRepository, IRoleRepository roleRepository, ILogger<CustomAuthorizationHandler> logger)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            foreach (var requirement in pendingRequirements)
            {
                if (requirement is CustomRequirement)
                {
                    if (await IsHasClaim(context.User, context.Resource, (CustomRequirement) requirement))
                    {
                        _logger.LogInformation("Truy cập được");
                        context.Succeed(requirement);
                    }
                    _logger.LogError("Không có quyền truy cập");

                }
            }
        }

        private async Task<bool> IsHasClaim(ClaimsPrincipal user, object? resource, CustomRequirement requirement)
        {
            // lấy user
            var userId = user.FindFirstValue("Id");
            var userResult = await _accountRepository.FindByIdAsync(userId);

            if (userResult != null)
            {
                var roleClaims = await _accountRepository.GetClaimsByUserRolesAsync(userResult);
                if(roleClaims != null)
                {
                    return roleClaims.Any(x => x.Type.Equals(requirement.RequiredClaimType) && x.Value.Equals(requirement.RequiredClaimValue));
                }
            }
            return true;

        }
    }
}
