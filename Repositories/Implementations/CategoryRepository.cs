using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category categorymodel)
        {
            await _context.Categories.AddAsync(categorymodel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(Category categorymodel)
        {
            _context.Categories.Remove(categorymodel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category categorymodel)
        {
            _context.Categories.Update(categorymodel);
            await _context.SaveChangesAsync();
        }
    }
}
