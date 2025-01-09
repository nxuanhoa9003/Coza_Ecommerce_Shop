using Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels
{
    public class ProductCategoryViewModel
    {
        public IPagedList<ProductCategory>? ProductCategories { get; set; }
        public PagingViewModel PagingInfo { get; set; }
    }
}
