using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class AttributesRepository : IAttributesRepository
    {
        private readonly AppDbContext _context;
        public AttributesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Attributes attributemodel)
        {
            await _context.Attributes.AddAsync(attributemodel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attributes>> GetAllAsync()
        {
            return await _context.Attributes.Include(x => x.AttributeValues).ToListAsync();
        }

        public async Task<Attributes?> GetByIdAsync(int? id)
        {
            return await _context.Attributes.Include(x => x.AttributeValues).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task RemoveAsync(Attributes attributemodel)
        {
            _context.Attributes.Remove(attributemodel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attributes attributemodel)
        {
            _context.Attributes.Update(attributemodel);
            await _context.SaveChangesAsync();
        }
    }
}
