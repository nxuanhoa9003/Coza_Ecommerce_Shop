using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class NewRepository : INewRepository
    {
        private readonly AppDbContext _context;
        public NewRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(New newmodel)
        {
            await _context.News.AddAsync(newmodel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<New>> GetAllAsync()
        {
            return await _context.News.Include(x => x.Category).ToListAsync();
        }

        public async Task<New?> GetByIdAsync(Guid? id)
        {
            return await _context.News.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(New newmodel)
        {
            _context.News.Remove(newmodel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync (IEnumerable<New> listnews)
        {
            _context.News.RemoveRange(listnews);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(New newmodel)
        {
            _context.News.Update(newmodel);
            await _context.SaveChangesAsync();
        }
    }
}
