using Coza_Ecommerce_Shop.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Extentions
{
    public static class AdminClaimSeeder
    {
        public static async Task EnsureAdminHasFullClaims(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser != null)
            {
                var existingClaims = await userManager.GetClaimsAsync(adminUser);
                var allPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().Select(p => p.ToString());

                var missingClaims = allPermissions.Except(existingClaims.Select(c => c.Value)).ToList();

                if (missingClaims.Any())
                {
                    foreach (var claim in missingClaims)
                    {
                        await userManager.AddClaimAsync(adminUser, new Claim("Permission", claim));
                    }
                    Console.WriteLine("Admin claims updated successfully.");
                }else
                {
                    Console.WriteLine("Admin claims full.");
                }
            }
        }
    }
}
