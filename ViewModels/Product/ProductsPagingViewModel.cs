using Entities = Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductsPagingViewModel
    {
        public IPagedList<Entities.Product>? Products { get; set; }
        public PagingViewModel PagingInfo { get; set; }
    }
}
