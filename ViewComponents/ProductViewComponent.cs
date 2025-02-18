using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Home;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {

        private readonly IProductCategoryService _productCategoryService;
        public ProductViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ParamsFilter? paramsFilter, int pageSize = 4)
        {
            int page = paramsFilter?.page ?? 1;
            var model = await _productCategoryService.GetProductCategoriesAsync(page, pageSize, paramsFilter);
			model.page = page;
			model.pagingViewModel = new PagingViewModel
			{
				CurrentPage = page,
				PageSize = pageSize,
				TotalCount = model.totalProducts,
				TotalPages = (int)Math.Ceiling(model.totalProducts / (double)pageSize),

				SearchTerm = paramsFilter?.search,
                Slug = paramsFilter?.slug,
				SortOrder = paramsFilter?.sort,
                PriceMin = paramsFilter?.minPrice,
                PriceMax = paramsFilter?.maxPrice
            };
            
			return View(model);
        }
    }
}
