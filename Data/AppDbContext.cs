using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Coza_Ecommerce_Shop.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            builder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            builder.Entity<ProductVariant>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            builder.Entity<AppUser>()
                .Property(u => u.UserName)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");
            builder.Entity<AppUser>()
                .Property(u => u.Email)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");

            //Nếu xóa giỏ hàng thì xóa luôn sản phẩm trong giỏ.
            builder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Chỉ giữ Cascade ở đây

            //Ngăn xóa sản phẩm hoặc biến thể khi xóa giỏ hàng.
            builder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction); // Ngăn xóa liên hoàn

            builder.Entity<CartItem>()
                .HasOne(c => c.Variant)
                .WithMany()
                .HasForeignKey(c => c.VariantId)
                .OnDelete(DeleteBehavior.NoAction); // Ngăn xóa liên hoàn


            builder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.NoAction); // Tránh xóa cascade

            builder.Entity<OrderDetail>()
                .HasOne(od => od.Variant)
                .WithMany(pv => pv.OrderDetails)
                .HasForeignKey(od => od.VariantId)
                .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Transaction>()
		        .HasKey(t => t.TransactionId);

			builder.Entity<Transaction>()
	           .HasOne(t => t.Order)
	           .WithOne(o => o.Transaction)
	           .HasForeignKey<Transaction>(t => t.OrderId)
	           .OnDelete(DeleteBehavior.Cascade);

			foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<SettingConfiguration> SettingConfigurations { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ClaimsCustom> Claims { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
    }
}
