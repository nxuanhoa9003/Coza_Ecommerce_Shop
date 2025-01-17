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
        private readonly IAttributesRepository _attributesRepository;
        private readonly IAttributesValuesRepository _attributesValuesRepository;
        private readonly ILogger<QuickViewProductViewComponent> _logger;
        private readonly IMapper _mapper;
        public QuickViewProductViewComponent(IProductRepository productRepository, IAttributesRepository attributesRepository,
            IAttributesValuesRepository attributesValuesRepository, ILogger<QuickViewProductViewComponent> logger,
           IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
            _attributesRepository = attributesRepository;
            _attributesValuesRepository = attributesValuesRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
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

                var listattributesvalues = await _attributesValuesRepository.GetAllAsync();
                var attributeValueDictionary = listattributesvalues.ToDictionary(x => x.Id, x => x.Value);
                foreach (var variant in varaintsvm)
                {
                    if (!string.IsNullOrEmpty(variant.AttributesJson))
                    {
                        try
                        {
                            var objAttributes = JsonConvert.DeserializeObject<List<AttributeJsonToModel>>(variant.AttributesJson);
                            if (objAttributes != null)
                            {
                               
                                var listAttrbVaraint = new List<string>();
                                foreach (var item in objAttributes)
                                {

                                    var attributeValue = attributeValueDictionary.ContainsKey(item.AttributeValue)
                                        ? attributeValueDictionary[item.AttributeValue]
                                        : null;

                                    listAttrbVaraint.Add(attributeValue);
                                }

                                string strNameVaraint = string.Join(" - ", listAttrbVaraint);

                                variant.NameVariantFromAttributes = strNameVaraint;
                            }
                        }
                        catch (JsonException ex)
                        {

                            _logger.LogError($"Lỗi parse JSON: {ex.Message}");
                        }


                    }
                }
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
