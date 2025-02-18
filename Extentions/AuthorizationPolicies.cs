using Coza_Ecommerce_Shop.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Coza_Ecommerce_Shop.Extentions
{
    public static class AuthorizationPolicies
    {
        public static void AddCustomPolicies(AuthorizationOptions options)
        {
            foreach (Permission permission in Enum.GetValues(typeof(Permission)))
            {
                options.AddPolicy(permission.ToString(), policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new CustomRequirement("Permission", permission.ToString()));
                });
            }
        }

        public static void AddCustomPolicy(this AuthorizationOptions options, string permission)
        {
            options.AddPolicy(permission, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new CustomRequirement("Permission", permission));
            });
        }
    }
}
