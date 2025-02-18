using Coza_Ecommerce_Shop.Models;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class EmployeePagingViewModel
    {
        public IPagedList<AppUser>? listEmployees { get; set; }
        public PagingViewModel? PagingInfo { get; set; }
    }
}
