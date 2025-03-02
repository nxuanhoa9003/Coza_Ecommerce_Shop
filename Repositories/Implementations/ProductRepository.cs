using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Product productmodel)
        {
            await _context.Products.AddAsync(productmodel);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AddRangePrioductImageAsync(ICollection<ProductImage> listproductImages)
        {
            await _context.ProductImages.AddRangeAsync(listproductImages);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string?>> CheckDuplicateVariantSKUs(ICollection<ProductVariant> variants)
        {
            if (variants == null || variants.Count == 0)
                return new List<string?>();

            var variantSKUs = variants.Select(v => v.SKU).ToList();
            var rs = await _context.ProductVariants.Where(v => variantSKUs.Contains(v.SKU))
                                 .Select(v => v.SKU)
                                 .ToListAsync();
            return rs;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.ProductCategory)
                .Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid? id)
        {
            return await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Variants).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<Product?> GetDetailProductByIdAsync(Guid? id)
        {
            var product = await _context.Products.AsNoTracking()
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (product != null)
            {
                product.Variants = product.Variants.OrderBy(v => v.SKU).ToList(); // Sắp xếp Variants theo SKU
                product.ProductImages = product.ProductImages.OrderByDescending(v => v.IsDefault).ToList();
            }

            return product;

        }


        public async Task<Product?> GetDetailProductBySlugAsync(string slug)
        {
            var product = await _context.Products.AsNoTracking()
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(m => m.Slug == slug && !m.IsDeleted);

            if (product != null)
            {
                product.Variants = product.Variants.OrderBy(v => v.SKU).ToList(); // Sắp xếp Variants theo SKU
                product.ProductImages = product.ProductImages.OrderByDescending(v => v.IsDefault).ToList();
            }


            return product;

        }


        public async Task<Product?> GetDetailProductBySKUAsync(string sku)
        {
            var product = await _context.Products.AsNoTracking()
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(m => m.ProductCode == sku && !m.IsDeleted);

            if (product != null)
            {
                product.Variants = product.Variants.OrderBy(v => v.SKU).ToList(); // Sắp xếp Variants theo SKU
                product.ProductImages = product.ProductImages.OrderByDescending(v => v.IsDefault).ToList();
            }
            return product;
        }



        public async Task<bool> IsDuplicateProductCode(Product productmodel)
        {
            return await _context.Products.AnyAsync(p => p.ProductCode == productmodel.ProductCode);
        }



        public async Task RemoveAsync(Product productmodel)
        {
            productmodel.IsDeleted = true;
            _context.Products.Update(productmodel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeProductImageByIDAsync(IEnumerable<Guid> imageIds)
        {

            if (imageIds != null && imageIds.Any())
            {
                var productImages = await _context.ProductImages
                    .Where(image => imageIds.Contains(image.Id))
                    .ToListAsync();
                if (productImages.Count > 0)
                {
                    foreach (var image in productImages)
                    {
                        Utilities.DeleteImage(image.Image);
                    }
                    _context.ProductImages.RemoveRange(productImages);
                    await _context.SaveChangesAsync();
                }
            }
        }


        public async Task UpdateAsync(Product productmodel)
        {
            // product image
            var newImages = productmodel.ProductImages;
            var existingImages = _context.ProductImages.Where(x => x.ProductId == productmodel.Id).ToList();

            var imagesToDelete = existingImages.Where(x => !newImages.Any(y => y.Id == x.Id)).ToList();

            // Xóa ảnh khỏi hệ thống tệp trước khi xóa khỏi database
            foreach (var image in imagesToDelete)
            {
                Utilities.DeleteImage(image.Image);
                _context.ProductImages.Remove(image);
            }

            foreach (var newImage in newImages)
            {
                var existingImage = existingImages.FirstOrDefault(x => x.Id == newImage.Id);
                if (existingImage == null)
                {
                    _context.Add(newImage);
                }
                else
                {
                    existingImage.IsDefault = newImage.IsDefault;
                    _context.Entry(existingImage).State = EntityState.Modified;
                }
            }


            // product variants
            var newVariants = productmodel.Variants;
            var existingVariants = _context.ProductVariants.Where(x => x.ProductId == productmodel.Id).ToList();

            foreach (var newVariant in newVariants)
            {
                var existingVariant = existingVariants.FirstOrDefault(x => x.Id == newVariant.Id);
                if (existingVariant == null)
                {

                    _context.Add(newVariant);
                }
                else
                {
                    existingVariant.SKU = newVariant.SKU;
                    existingVariant.Color = newVariant.Color;
                    existingVariant.Size = newVariant.Size;
                    existingVariant.BasePrice = newVariant.BasePrice;
                    existingVariant.PriceSale = newVariant.PriceSale;
                    existingVariant.Quantity = newVariant.Quantity;
                    existingVariant.IsActive = newVariant.IsActive;
                    existingVariant.IsDefault = newVariant.IsDefault;

                    _context.Entry(existingVariant).State = EntityState.Modified;
                }
            }

            _context.Entry(productmodel).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {innerException}");
                throw;
            }


        }



        public async Task<IEnumerable<ProductImage>> GetProductImagesByIdProduct(Guid? id)
        {
            return await _context.ProductImages.Where(x => x.ProductId == id).ToListAsync();
        }

        public async Task UpdateRangeImagesAsync(IEnumerable<ProductImage> listProductsImages)
        {
            _context.ProductImages.UpdateRange(listProductsImages);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            return await _context.Products
                .Include(x => x.ProductCategory)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByIdProductCategoryAsync(HashSet<Guid> allCategoryIds)
        {

            var products = await _context.Products
                                .Include(x => x.ProductCategory)
                                .Include(x => x.Variants)
                                .AsNoTracking()
                                .Where(x => allCategoryIds.Contains(x.ProductCategory.Id))
                                .OrderByDescending(x => x.IsSale)
                                .ThenByDescending(x => x.IsHot)
                                .ToListAsync();
            return products;
        }


        public async Task<IEnumerable<ProductVariant>> GetAllVariantsOfProduct(Guid? id)
        {
            return await _context.ProductVariants.AsNoTracking().Where(x => x.ProductId == id).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsRelated(Guid? CategoryId, string sku)
        {
            var listproduct = await _context.Products
                .Include(x => x.Variants)
                .Where(x => x.ProductCategoryId == CategoryId && x.ProductCode != sku).ToListAsync();
            return listproduct;
        }

        public async Task<IEnumerable<Product>> GetProductsHot()
        {
            var listproduct = await _context.Products.Include(x => x.Variants).AsNoTracking()
                .Where(x => !x.IsDeleted).OrderByDescending(x => x.IsHot).ToListAsync();
            return listproduct;
        }

        public async Task<List<string>> GetWishlistByUserid(string userid)
        {
            var wishlist = await _context.Wishlists.Where(x => x.UserId == userid)
                .Select(x => x.ProductId.ToString()).ToListAsync();
            return wishlist;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddToWishlist(Wishlist wishlist)
        {
            if (wishlist == null) return (false, "Thêm thất bại");
            var checkexist = await _context.Wishlists.FirstOrDefaultAsync(x => x.UserId == wishlist.UserId && x.ProductId == wishlist.ProductId);
            if (checkexist != null)
            {
                _context.Wishlists.Remove(checkexist);
                await _context.SaveChangesAsync();
                return (true, "Sản phẩm đã được xoá khỏi danh mục yêu thích");
            }
            await _context.Wishlists.AddAsync(wishlist);
            var rs = await _context.SaveChangesAsync();
            return rs > 1 ? (true, "Sản phẩm đã được thêm vào danh mục yêu thích") : (false, "Thêm thất bại");
        }

        public async Task<IEnumerable<Product>> GetProductWishlistByUserid(string userid)
        {
            return await _context.Wishlists
                .Include(x => x.product)
                .ThenInclude(x => x.Variants)
                .Select(x => x.product).ToListAsync();
        }

    }
}
