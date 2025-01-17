using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels.Home;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int? id);
        Task<Product?> GetDetailProductByIdAsync(int? id);
        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize);
        Task<ProductOverViewVM> GetProductByIdProductCategoryAsync(IEnumerable<ProductCategoryDTO> productCategoryDTOs, int page, int pageSize);

        Task AddAsync(Product productmodel);
        Task AddRangePrioductImageAsync(ICollection<ProductImage> listproductImages);

        Task RemoveAsync(Product productmodel);
        Task RemoveRangeAsync(IEnumerable<Product> listproducts);
        Task UpdateAsync(Product productmodel);
        Task <bool> IsDuplicateProductCode(Product productmodel);
        Task <bool> ProductExists(int id);
        Task <IEnumerable<string?>> CheckDuplicateVariantSKUs(ICollection<ProductVariant> variants);
        Task RemoveRangeProductImageByIDAsync(IEnumerable<int> imageIds);

        Task<IEnumerable<ProductImage>> GetProductImagesByIdProduct(int? id);
        Task UpdateRangeImagesAsync(IEnumerable<ProductImage> listProductsImages);
    }
}
