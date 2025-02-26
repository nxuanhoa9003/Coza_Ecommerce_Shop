using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ClaimsPrincipal?> GetUserBySchemeAsync(string scheme)
        {
            var authResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(scheme);
            return authResult?.Principal;
        }

        public async Task<ClaimsPrincipal?> GetUserByUserTypeAsync(string userType)
        {
            string scheme = userType.ToLower().Equals("admin") ? "AdminScheme" : "CustomerScheme";
            var user = await GetUserBySchemeAsync(scheme);
            return user;
        }

        public async Task<bool> IsUserTypeAsync(string userType)
        {
            string scheme = userType.ToLower().Equals("admin") ? "AdminScheme" : "CustomerScheme";
            var user = await GetUserBySchemeAsync(scheme);
            return user?.HasClaim(c => c.Type == "UserType" && c.Value == userType) ?? false;
        }
    }
}
