using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IAttributesValuesRepository
    {
        Task<AttributeValue?> GetByIdAsync(int? id);
        Task<IEnumerable<AttributeValue>> GetAllAsync();
        Task AddAsync(AttributeValue attributevaluemodel);
        Task AddRangeAsync(IEnumerable<AttributeValue> listattributevalues);
        Task RemoveAsync(AttributeValue attributevaluemodel);
        Task RemoveRangeAsync(IEnumerable<AttributeValue> listattributevalues);
        Task UpdateAsync(AttributeValue attributevaluemodel);
        Task UpdateRangeAsync(IEnumerable<AttributeValue> listattributevalues);

        Task<IEnumerable<AttributeValue>> GetListByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<AttributeValue>> GetAttributeValuesByIdAttributeAsync(int id);


    }
}
