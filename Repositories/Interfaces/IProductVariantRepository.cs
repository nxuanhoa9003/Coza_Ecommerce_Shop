using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IProductVariantRepository
    {
        Task<ProductVariant?> GetByIdAsync(int? id);
        Task<ProductVariant?> GetBySkuAsync(string? SKU);
        
        Task<IEnumerable<ProductVariant>> GetAllAsyncByProductID(Product product);
        Task AddAsync(ProductVariant productvariantmodel);
        Task AddARangesync(ICollection<ProductVariant> productvariantmodel);

        Task RemoveAsync(ProductVariant productvariantmodel);
        Task RemoveRangeAsync(ICollection<ProductVariant> listproductvariantmodel);
        Task UpdateAsync(ProductVariant productvariantmodel);
        

       
    }
}
