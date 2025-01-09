using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class AttributesValuesRepository : IAttributesValuesRepository
    {
        private readonly AppDbContext _context;
        public AttributesValuesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AttributeValue attributevaluemodel)
        {
            await _context.AttributeValues.AddAsync(attributevaluemodel);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<AttributeValue> listattributevalues)
        {
            await _context.AttributeValues.AddRangeAsync(listattributevalues);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AttributeValue>> GetAllAsync()
        {
            return await _context.AttributeValues.ToListAsync();
        }

        public Task<AttributeValue?> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AttributeValue>> GetListByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<AttributeValue>();
            }

            return await _context.AttributeValues
                .Where(av => ids.Contains(av.Id))
                .ToListAsync();
        }

        public async Task RemoveAsync(AttributeValue attributevaluemodel)
        {
            _context.AttributeValues.Remove(attributevaluemodel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<AttributeValue> listattributevalues)
        {
            _context.AttributeValues.RemoveRange(listattributevalues);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AttributeValue attributevaluemodel)
        {
            _context.AttributeValues.Update(attributevaluemodel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<AttributeValue> listattributevalues)
        {
            _context.AttributeValues.UpdateRange(listattributevalues);
            await _context.SaveChangesAsync();
        }
    }
}
