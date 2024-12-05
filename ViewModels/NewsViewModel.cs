using Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels
{
    public class NewsViewModel
    {
        public IPagedList<New>? News { get; set; }
        public PagingViewModel PagingInfo { get; set; }
    }
}
