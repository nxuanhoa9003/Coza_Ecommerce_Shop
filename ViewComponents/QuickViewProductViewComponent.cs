using AutoMapper;
using Coza_Ecommerce_Shop.Areas.Admin.Controllers;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Home;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class QuickViewProductViewComponent : ViewComponent
    {
        private readonly IProductRepository _productRepository;
       
        private readonly ILogger<QuickViewProductViewComponent> _logger;
        private readonly IMapper _mapper;
        public QuickViewProductViewComponent(IProductRepository productRepository, ILogger<QuickViewProductViewComponent> logger,
           IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
           
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var product = await _productRepository.GetDetailProductByIdAsync(id);
            if (product == null)
            {
                return Content("Sản phẩm không tồn tại");
            }

            List<ProductVariantsViewModel> varaintsvm = new List<ProductVariantsViewModel>();

            if (product.Variants != null && product.Variants.Any())
            {
                varaintsvm = _mapper.Map<List<ProductVariantsViewModel>>(product.Variants);  
            }


            var quickViewProductVM = new QuickViewProductVM()
            {
                productInfo = product,
                images = product.ProductImages != null ? product.ProductImages : null,
                varaints = varaintsvm
            };

            return View("QuickViewProduct", quickViewProductVM);
        }
    }
}
