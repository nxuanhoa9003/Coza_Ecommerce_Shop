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
            return await _context.ProductCategories
                .Include(x => x.ParentCategory)
                .Include(x => x.CategoryChildren)
                .AsNoTracking().Where(c => !c.IsDeleted).ToListAsync();
        }


        public async Task<IEnumerable<ProductCategory>> GetAllExceptIdAsync(Guid? id)
        {
            return await _context.ProductCategories.Where(x => x.Id != id && !x.IsDeleted).ToListAsync();
        }

		public async Task<IEnumerable<ProductCategory>> GetByFilterSlugAsNoTrackingAsync(string slug)
        {
			return await _context.ProductCategories.Where(x => x.Slug == slug).ToListAsync();
		}

        public async Task<ProductCategory?> GetByIdAsNoTrackingAsync(Guid? id)
        {
            return await _context.ProductCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<ProductCategory?> GetByIdAsync(Guid? id)
        {
            return await _context.ProductCategories.Include(x => x.ParentCategory).FirstOrDefaultAsync(x => x.Id == id);
        }

		public async Task<ProductCategory?> GetBySlugAsNoTrackingAsync(string slug)
		{
			return await _context.ProductCategories.FirstOrDefaultAsync(x => x.Slug == slug);
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
