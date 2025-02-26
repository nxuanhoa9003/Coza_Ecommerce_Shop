using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Extentions;
using Coza_Ecommerce_Shop.Mappings;
using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Coza_Ecommerce_Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                string strconnect = builder.Configuration.GetConnectionString("HShop");
                option.UseSqlServer(strconnect);
            });

            // Add services Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            // Truy cập IdentityOptions
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = true; // Không bắt phải có số
                options.Password.RequireLowercase = true; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = true; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = true; // Không bắt buộc chữ in
                options.Password.RequiredLength = 6; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lần thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất


                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;

            });


            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(1); // Token chỉ có hiệu lực trong 5 phút
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "CustomerScheme"; // Scheme mặc định
                options.DefaultSignInScheme = "CustomerScheme";
            })
            .AddCookie("CustomerScheme", options =>
            {
                options.LoginPath = "/login/";
                options.LogoutPath = "/logout/";
                options.AccessDeniedPath = "/access-denied";
                options.Cookie.Name = "CustomerAuth";
            })
            .AddCookie("AdminScheme", options =>
            {
                options.LoginPath = "/Admin/Account/Login";
                options.LogoutPath = "/Admin/logout/";
                options.AccessDeniedPath = "/Admin/AccessDenied";
                options.Cookie.Name = "AdminAuth";
            })
            .AddGoogle("GoogleScheme",options =>
            {
                IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
                options.CallbackPath = "/dang-nhap-tu-google";
                options.SignInScheme = "CustomerScheme";
            });

            builder.Services.AddHttpContextAccessor();


            // Add services MailSettings
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            // add authorizationPolicies
            builder.Services.AddAuthorization(options =>
            {
                AuthorizationPolicies.AddCustomPolicies(options);
            });

            // Register Services
            builder.Services.AddApplicationServices();

            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseNotyf();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
                ),
                RequestPath = "/Uploads"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
