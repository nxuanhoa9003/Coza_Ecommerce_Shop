using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Coza_Ecommerce_Shop.Extentions
{
    public class CustomUserManager : UserManager<AppUser>
    {
        private readonly AppDbContext _context;

        public CustomUserManager(
            IUserStore<AppUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<AppUser>> logger,
            AppDbContext context)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
                   keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

        public override async Task<AppUser?> FindByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public override async Task<AppUser?> FindByNameAsync(string userName)
        {
            return await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

           /* return await _context.Users
            .Where(u => EF.Functions.Collate(u.UserName, "SQL_Latin1_General_CP1_CS_AS") == userName)
            .FirstOrDefaultAsync();*/
        }

    }
}
