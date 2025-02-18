using Coza_Ecommerce_Shop.ViewModels.Home;
using Coza_Ecommerce_Shop.ViewModels.Product;

namespace Coza_Ecommerce_Shop.Services
{
    public interface IProductCategoryService
    {
        Task<ProductOverViewVM> GetProductCategoriesAsync(int page, int pageSize, ParamsFilter? filter = null);

    }
}
