using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _context;

        public ProductVariantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductVariant productvariantmodel)
        {
            await _context.AddAsync(productvariantmodel);
            await _context.SaveChangesAsync();
        }

        public async Task AddARangesync(ICollection<ProductVariant> listproductvariantmodel)
        {
            await _context.AddRangeAsync(listproductvariantmodel);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<ProductVariant>> GetAllAsyncByProductID(Product product)
        {
            return await _context.ProductVariants.Where(x => x.ProductId == product.Id).ToListAsync();
        }

        public Task<ProductVariant?> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(ProductVariant productvariantmodel)
        {
            _context.Remove(productvariantmodel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(ICollection<ProductVariant> listproductvariantmodel)
        {
            _context.RemoveRange(listproductvariantmodel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductVariant productmodel)
        {
            _context.Update(productmodel);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductVariant?> GetBySkuAsync(string SKU)
        {
            return await _context.ProductVariants.Include(x=> x.Product).AsNoTracking().FirstOrDefaultAsync(x => x.SKU == SKU);
        }
    }
}
