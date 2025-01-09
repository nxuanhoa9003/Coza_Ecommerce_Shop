using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IAttributesRepository
    {
        Task<Attributes?> GetByIdAsync(int? id);
        Task<IEnumerable<Attributes>> GetAllAsync();
        Task AddAsync(Attributes attributemodel);

        Task RemoveAsync(Attributes attributemodel);
        Task UpdateAsync(Attributes attributemodel);
    }
}
