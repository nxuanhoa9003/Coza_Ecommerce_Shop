using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.ViewComponents
{
	public class ProductViewComponent : ViewComponent
	{
		private readonly IProductRepository _productRepository;
		private readonly IProductCategoryRepository _productCategoryRepository;

		public ProductViewComponent(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
		{
			_productRepository = productRepository;
			_productCategoryRepository = productCategoryRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync(int page = 1, int pageSize = 16)
		{
			var ProductCategoryFeatured = await _productCategoryRepository.GetAllProductCategoryFeatured();
			var ProductOverViewVM = (await _productRepository.GetProductByIdProductCategoryAsync(ProductCategoryFeatured, page, pageSize));
            return View(ProductOverViewVM);
		}
	}
}
