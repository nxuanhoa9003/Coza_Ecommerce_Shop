using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class UserPagingViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagingViewModel model)
        {
            return Task.FromResult((IViewComponentResult)View(model));
        }
    }
}
