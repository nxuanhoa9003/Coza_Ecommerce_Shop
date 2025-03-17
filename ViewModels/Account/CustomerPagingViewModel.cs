using Coza_Ecommerce_Shop.Models;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class CustomerPagingViewModel
    {
        public IPagedList<AppUser>? listCustomers { get; set; }
        public PagingViewModel? PagingInfo { get; set; }
    }
}
