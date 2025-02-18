using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private readonly IProductRepository _productRepository;
        public RelatedProductsViewComponent(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid? CategoryId, string sku)
        {
            var listproduct = await _productRepository.GetProductsRelated(CategoryId, sku);
            return View(listproduct);
        }

    }
}
