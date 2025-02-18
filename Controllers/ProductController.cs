using AutoMapper;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductCategoryRepository productCategoryRepository, IProductRepository productRepository, IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productRepository = productRepository;
            _mapper = mapper;
            _productService = productService;
			_productCategoryRepository = productCategoryRepository;
		}

        [HttpGet("product-detail/{slug}")]
        public async Task<IActionResult> ProductDetail(string slug)
        {
            var product = await _productRepository.GetDetailProductBySlugAsync(slug);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
            
        }

        [HttpGet("list-product")]
        public async Task<IActionResult> ListProduct([FromQuery] ParamsFilter filter)
        {
            var breadCrumbShow = "Sản phẩm";
            var productsListViewModel = new ProductsListViewModel();

			if (!string.IsNullOrEmpty(filter.slug))
            {
                var productCategory = await _productCategoryRepository.GetBySlugAsNoTrackingAsync(filter.slug);
                if (productCategory != null) 
                {
				    breadCrumbShow = productCategory.Title;
				}
            }
			productsListViewModel.breadCrumbShow = breadCrumbShow;
			productsListViewModel.paramsFilter = filter;

			return View(productsListViewModel);
        }

        [HttpGet("load-more-product")]
        public async Task<IActionResult> LoadMore(int? page = 1)
        {
            ++page;
            int pageSize = 4;

            var data = await _productService.GetProductByPage(page.Value, pageSize);
            return Json(new
            {
                products = data,
                nextPage = page
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            });

        }


        [HttpGet("GetVariant")]
		public async Task<IActionResult> GetVariant(string sku, string color, string size)
		{
			var product = await _productRepository.GetDetailProductBySKUAsync(sku);
			if (product == null)
			{
				return NotFound();
			}

			var selectedVariant = product.Variants
								.FirstOrDefault(v => v.Color == color && v.Size == size);
			if (selectedVariant == null)
			{
				return NotFound("Variant không tồn tại");
			}

            var ObjVariant = _mapper.Map<ProductVariantsViewModel>(selectedVariant);


			return Json(new
			{
				variant = ObjVariant
			});

		}

        [HttpGet("get-variants")]
        public async Task<IActionResult> GetVariantsForProduct(string sku)
        {
            var product = await _productRepository.GetDetailProductBySKUAsync(sku);
            if (product == null)
            {
                return NotFound();
            }

            var selectedVariant = product.Variants;
            if (selectedVariant == null)
            {
                return NotFound("Variant không tồn tại");
            }

            var variants = product.Variants.Select(v => new
            {
                size = v.Size,
                color = v.Color,
                stock = v.IsActive ? v.Quantity : 0
            }).ToList();

            return Json(new { variants });
        }

    }
}
