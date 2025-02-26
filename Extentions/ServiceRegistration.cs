using Coza_Ecommerce_Shop.Authorization;
using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Models.Helper;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Coza_Ecommerce_Shop.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UserManager<AppUser>, CustomUserManager>();

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<INewRepository, NewRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
           
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();

            // register mail service
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<EmailTemplateService>();

            // Register the handler authorzationhandler
            services.AddScoped<IAuthorizationHandler, CustomAuthorizationHandler>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            // VNPAY
            services.AddScoped<IVnPayService, VnPayService>();

            services.AddScoped<UserService>();

        }
    }
}
