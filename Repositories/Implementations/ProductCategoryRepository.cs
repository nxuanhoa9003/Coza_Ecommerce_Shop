using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.DTO;
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

        public async Task<bool> AddAsync(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.ProductCategories.Include(x => x.ParentCategory).Where(c => !c.IsDeleted).ToListAsync();
        }


        public async Task<IEnumerable<ProductCategory>> GetAllExceptIdAsync(int? id)
        {
            return await _context.ProductCategories.Where(x => x.Id != id && !x.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetAllProductCategoryFeatured()
        {
            return await _context.ProductCategories.Where(x => x.IsFeatured).Select(x => new ProductCategoryDTO
            {
                Id = x.Id,
                Name = x.Title
            }).ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetByFilterSlugAsNoTrackingAsync(string slug)
        {
            return await _context.ProductCategories.AsNoTracking().Where(x => x.Slug == slug).ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsNoTrackingAsync(int? id)
        {
            return await _context.ProductCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<ProductCategory?> GetByIdAsync(int? id)
        {
            return await _context.ProductCategories.Include(x => x.ParentCategory).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsCategoryExistsAsync(ProductCategory pnew, ProductCategory? pold = null)
        {
            if (pold != null)
            {
                return await _context.ProductCategories.AsNoTracking()
                                    .AnyAsync(c => c.Title == pnew.Title
                                    && c.ParentCategoryId == pnew.ParentCategoryId
                                    && c.Id != pold.Id
                                    );
            }
            return await _context.ProductCategories.AsNoTracking()
                    .AnyAsync(c => c.Title == pnew.Title && c.ParentCategoryId == pnew.ParentCategoryId);
        }

        public async Task<bool> RemoveAsync(ProductCategory productCategory)
        {
            productCategory.IsDeleted = true;
            //_context.ProductCategories.Remove(productCategory);
            _context.ProductCategories.Update(productCategory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
