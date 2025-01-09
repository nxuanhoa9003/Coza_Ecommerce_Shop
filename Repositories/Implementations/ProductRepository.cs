using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product productmodel)
        {
            await _context.Products.AddAsync(productmodel);
            await _context.SaveChangesAsync();
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
            return await _context.Products.Include(x => x.ProductCategory).Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int? id)
        {
            return await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<Product?> GetDetailProductByIdAsync(int? id)
        {
            return await _context.Products.AsNoTracking()
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<bool> IsDuplicateProductCode(Product productmodel)
        {
            return await _context.Products.AnyAsync(p => p.ProductCode == productmodel.ProductCode);
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task RemoveAsync(Product productmodel)
        {
            productmodel.IsDeleted = true;
            _context.Products.Update(productmodel);
            await _context.SaveChangesAsync();

        }

        public async Task RemoveRangeProductImageByIDAsync(IEnumerable<int> imageIds)
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

        public Task RemoveRangeAsync(IEnumerable<Product> listproducts)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product productmodel)
        {
            _context.Products.Update(productmodel);
            await _context.SaveChangesAsync();
        }

        

        public async Task<IEnumerable<ProductImage>> GetProductImagesByIdProduct(int? id)
        {
            return await _context.ProductImages.Where(x => x.ProductId == id).ToListAsync();
        }

        public async Task UpdateRangeImagesAsync(IEnumerable<ProductImage> listProductsImages)
        {
            _context.ProductImages.UpdateRange(listProductsImages);
            await _context.SaveChangesAsync();
        }
    }
}
