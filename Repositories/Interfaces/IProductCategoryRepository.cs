using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory?> GetByIdAsync(int? id);
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task AddAsync(ProductCategory productCategory);

        Task RemoveAsync(ProductCategory productCategory);
        Task UpdateAsync(ProductCategory productCategory);
    }
}
