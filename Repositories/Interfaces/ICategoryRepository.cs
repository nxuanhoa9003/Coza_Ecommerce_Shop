using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid? id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category categorymodel);

        Task RemoveAsync(Category categorymodel);
        Task UpdateAsync(Category categorymodel);
    }
}
