using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface INewRepository
    {
        Task<New?> GetByIdAsync(Guid? id);
        Task<IEnumerable<New>> GetAllAsync();
        Task AddAsync(New newmodel);

        Task RemoveAsync(New newmodel);
        Task RemoveRangeAsync(IEnumerable<New> listnews);
        Task UpdateAsync(New newmodel);
    }
}
