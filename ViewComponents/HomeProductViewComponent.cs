using Microsoft.AspNetCore.Mvc;
using Coza_Ecommerce_Shop.Services;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class HomeProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public HomeProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int pageSize = 4)
        {
            var model = await _productService.GetProductByPage(page, pageSize);

            return View(model);
        }
    }
}
