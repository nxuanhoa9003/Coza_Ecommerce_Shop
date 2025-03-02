using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Coza_Ecommerce_Shop.ViewModels.Home;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid? id);
        Task<Product?> GetDetailProductByIdAsync(Guid? id);
        Task<Product?> GetDetailProductBySlugAsync(string slug);
        Task<Product?> GetDetailProductBySKUAsync(string sku);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetProductsRelated(Guid? CategoryId, string sku);
        Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize);
        Task<IEnumerable<Product>> GetProductByIdProductCategoryAsync(HashSet<Guid> allCategoryIds);

        Task<bool> AddAsync(Product productmodel);
        Task AddRangePrioductImageAsync(ICollection<ProductImage> listproductImages);

        Task RemoveAsync(Product productmodel);

        Task UpdateAsync(Product productmodel);
        Task<bool> IsDuplicateProductCode(Product productmodel);

        Task<IEnumerable<string?>> CheckDuplicateVariantSKUs(ICollection<ProductVariant> variants);
        Task RemoveRangeProductImageByIDAsync(IEnumerable<Guid> imageIds);

        Task<IEnumerable<ProductImage>> GetProductImagesByIdProduct(Guid? id);
        Task UpdateRangeImagesAsync(IEnumerable<ProductImage> listProductsImages);

        Task<IEnumerable<ProductVariant>> GetAllVariantsOfProduct(Guid? id);

        Task<IEnumerable<Product>> GetProductsHot();


        // add product to wishlist
        Task<List<string>> GetWishlistByUserid(string userid);
        Task<IEnumerable<Product>> GetProductWishlistByUserid(string userid);
        Task<(bool IsSuccess, string ErrorMessage)> AddToWishlist(Wishlist wishlist);

    }
}
