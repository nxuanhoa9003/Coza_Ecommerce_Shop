using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class ShowProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ShowProductCategoryViewComponent(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool? IsDesktop)
        {
            var categories = (await _productCategoryRepository.GetAllAsync()).ToList();
            var categoryTree = BuildCategoryTree(categories, null);
            var TypeMenuViewModel = new TypeMenuViewModel
			{
				IsDesktop = IsDesktop ?? false,
				Categories = categoryTree
            };
			return View(TypeMenuViewModel);
        }

        private List<ProductCategory> BuildCategoryTree(List<ProductCategory> categories, Guid? parentId)
        {
            var result = new List<ProductCategory>();

            foreach (var category in categories.Where(c => c.ParentCategoryId == parentId))
            {
                category.CategoryChildren = BuildCategoryTree(categories, category.Id);
                result.Add(category);
            }

            return result;
        }
    }
}
