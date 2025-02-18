using X.PagedList;
using Entities = Coza_Ecommerce_Shop.Models.Entities;
namespace Coza_Ecommerce_Shop.ViewModels.ProductCategory
{
    public class ProductCategoryPagingViewModel
    {
        public IPagedList<Entities.ProductCategory>? ProductCategories { get; set; }
        public PagingViewModel? PagingInfo { get; set; }
    }
}
