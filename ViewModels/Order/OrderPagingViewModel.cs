using Entities = Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels.Order
{
    public class OrderPagingViewModel
    {
        public IPagedList<Entities.Order>? Orders { get; set; }
        public PagingViewModel PagingInfo { get; set; }
    }
}
