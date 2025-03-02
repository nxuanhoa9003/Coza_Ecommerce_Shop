using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class HomeNewsViewComponent : ViewComponent
    {
        private readonly INewRepository _newRepository;

        public HomeNewsViewComponent(INewRepository newRepository)
        {
            _newRepository = newRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _newRepository.GetNewsHomeAsync();
            return View(model);
        }
    }
}
