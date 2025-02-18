using Microsoft.AspNetCore.Authorization;

namespace Coza_Ecommerce_Shop.Authorization
{
    public class CustomRequirement : IAuthorizationRequirement
    {
        public string RequiredClaimType { get; }
        public string RequiredClaimValue { get; }

        public CustomRequirement(string requiredClaimType, string requiredClaimValue)
        {
            RequiredClaimType = requiredClaimType;
            RequiredClaimValue = requiredClaimValue;
        }
    }
}
