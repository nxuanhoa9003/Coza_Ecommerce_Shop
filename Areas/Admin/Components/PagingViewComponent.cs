using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.Areas.Admin.Components
{
    public class PagingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PagingViewModel model)
        {
            return View(model); 
        }
    }
}
