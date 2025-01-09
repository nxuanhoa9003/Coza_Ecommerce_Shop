using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext _context;

        public ProductCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
           return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int? id)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
        }
    }
}
