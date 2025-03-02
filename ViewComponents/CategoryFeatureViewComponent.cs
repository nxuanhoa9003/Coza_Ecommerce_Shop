using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
	public class CategoryFeatureViewComponent : ViewComponent
	{
		private readonly IProductCategoryRepository _productCategory;

		public CategoryFeatureViewComponent(IProductCategoryRepository productCategory)
		{
			_productCategory = productCategory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listproductcategories = await _productCategory.GetAllAsync();
			var result = listproductcategories.Where(x => x.IsFeatured && x.ParentCategoryId == null).Take(5).ToList();
			return View(result);
		}
	}
}
