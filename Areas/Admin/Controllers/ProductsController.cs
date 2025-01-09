using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.ViewModels;
using System.Collections;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Product;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.CodeAnalysis;
using AttributeData = Coza_Ecommerce_Shop.ViewModels.Product.AttributeData;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IAttributesRepository _attributesRepository;
        private readonly IAttributesValuesRepository _attributesValuesRepository;
        private readonly IProductVariantRepository _productVariantRepository;

        private readonly ILogger<ProductsController> _logger;
        public INotyfService _notifyService { get; }

        public ProductsController
        (
            AppDbContext context,
            ILogger<ProductsController> logger,
            IMapper mapper, IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IAttributesRepository attributesRepository,
            IAttributesValuesRepository attributesValuesRepository,
            IProductVariantRepository productVariantRepository,
            INotyfService notifyService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _attributesRepository = attributesRepository;
            _attributesValuesRepository = attributesValuesRepository;
            _productVariantRepository = productVariantRepository;
            _notifyService = notifyService;
        }

        [HttpPost]
        [Route("/GetValuesAttribute")]
        public IActionResult GetValuesAttribute([FromForm] int attributeId)
        {
            var listvalue = _context.AttributeValues.Where(x => x.AttributeId == attributeId)
                 .Select(x => new
                 {
                     x.Id,
                     x.Value
                 })
                .ToList();

            return Json(new { success = true, data = listvalue });

        }


        // GET: Admin/Products
        public async Task<IActionResult> Index(string search)
        {
            var listproducts = await _productRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                listproducts = listproducts.Where(x => x.Title.Contains(search) || (x.ProductCode != null && x.ProductCode.Contains(search)));
            }

            listproducts = listproducts.OrderByDescending(x => x.Id);
            return View(listproducts);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productdetail = await _productRepository.GetDetailProductByIdAsync(id);

            if (productdetail == null)
            {
                return NotFound();
            }

            var listImages = productdetail?.ProductImages?.ToList();
            var listVariants = productdetail?.Variants?.ToList();

            var productDetailViewModel = new ProductDetailViewModel
            {
                ProductInfo = _mapper.Map<ProductDetailInfoViewModel>(productdetail),
                Images = _mapper.Map<List<ProductImageViewModel>>(listImages),
                Variants = _mapper.Map<List<ProductVariantsViewModel>>(listVariants)
            };

            if (productDetailViewModel.Variants != null)
            {
                var listattributes = await _attributesRepository.GetAllAsync();
                var listattributesvalues = await _attributesValuesRepository.GetAllAsync();

                var attributeDictionary = listattributes.ToDictionary(x => x.Id, x => x.AttributeName);
                var attributeValueDictionary = listattributesvalues.ToDictionary(x => x.Id, x => x.Value);

                foreach (var variant in productDetailViewModel.Variants)
                {
                    if (variant.AttributesJson != null)
                    {
                        try
                        {
                            var objAttributes = JsonConvert.DeserializeObject<List<AttributeJsonToModel>>(variant.AttributesJson);
                            if (objAttributes != null)
                            {
                                foreach (var item in objAttributes)
                                {
                                    var attributeName = attributeDictionary.ContainsKey(item.AttributeName)
                                         ? attributeDictionary[item.AttributeName]
                                         : null;

                                    var attributeValue = attributeValueDictionary.ContainsKey(item.AttributeValue)
                                        ? attributeValueDictionary[item.AttributeValue]
                                        : null;

                                    var ObjecAttributeData = new AttributeData
                                    {
                                        AttributeName = attributeName,
                                        Value = attributeValue,
                                    };

                                    variant.Attributes?.Add(ObjecAttributeData);
                                }
                            }
                        }
                        catch (JsonException ex)
                        {

                            _logger.LogError($"Lỗi parse JSON: {ex.Message}");
                        }


                    }
                }
            }

            return View(productDetailViewModel);
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            var listproductcategory = await _productCategoryRepository.GetAllAsync();
            var listAttributes = await _attributesRepository.GetAllAsync();
            ViewData["ProductCategoryId"] = new SelectList(listproductcategory, "Id", "Title");
            ViewData["Attributes"] = new SelectList(listAttributes, "Id", "AttributeName");
            return View();
        }


        private async Task InitializeViewData(Product product)
        {

            var listproductcategory = await _productCategoryRepository.GetAllAsync();
            var listattributes = await _attributesRepository.GetAllAsync();
            var selectedCategoryId = product?.ProductCategoryId ?? 0;


            ViewData["ProductCategoryId"] = new SelectList(listproductcategory.ToList(), "Id", "Title");
            ViewData["Attributes"] = new SelectList(listattributes.ToList(), "Id", "AttributeName");

            ViewData["ListAttributes"] = await _attributesRepository.GetAllAsync();
            ViewData["ListAtrributeValues"] = await _attributesValuesRepository.GetAllAsync();
        }


        private string GenerateProductSKU()
        {
            string guidPart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"PRD-{guidPart}";
        }

        private string GenerateSKUVariant(string productCode, int variantNumber)
        {
            return $"{productCode}-{variantNumber:D2}";
        }


        // POST: Admin/Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductCode,Title,Slug,Description,Detail,Image,Files,Price,PriceSale,Quantity,IsSale,IsFeature,IsHot,ProductCategoryId,SeoTitile,SeoDescription,SeoKeywords,IsActive")] Product product, List<ProductVariant> variants)
        {
            await InitializeViewData(product);
            if (ModelState.IsValid)
            {
                try
                {
                    // kiểm tra mã sku tồn tại
                    if (string.IsNullOrEmpty(product.ProductCode))
                    {
                        product.ProductCode = GenerateProductSKU();
                    }
                    else
                    {
                        if (await _productRepository.IsDuplicateProductCode(product))
                        {
                            ModelState.AddModelError("ProductCode", "Mã SKU của sản phẩm đã tồn tại.");

                            return View(product);
                        }
                    }

                    product.IsActive = true;
                    product.CreateDate = DateTime.Now;
                    product.ModifierDate = DateTime.Now;
                   

                    await _productRepository.AddAsync(product);
                    product.Slug = $"{FilterChar.GenerateSlug(product.Title)}-{product.Id}";
                    await _productRepository.UpdateAsync(product);

                    if (variants != null && variants.Count > 0)
                    {
                        List<ProductVariant> addedVariants = new List<ProductVariant>();
                        int variantNumber = 0;
                        foreach (var vr in variants)
                        {
                            if (!string.IsNullOrEmpty(vr.AttributesJson))
                            {
                                ++variantNumber;
                                ProductVariant pv = new ProductVariant
                                {
                                    ProductId = product.Id,
                                    SKU = GenerateSKUVariant(product.ProductCode, variantNumber),
                                    Quantity = vr.Quantity,
                                    AttributesJson = vr.AttributesJson,
                                    AdditionalPrice = vr.AdditionalPrice,
                                    CreateDate = DateTime.Now,
                                    ModifierDate = DateTime.Now,
                                    IsActive = true,
                                };

                                addedVariants.Add(pv);
                            }

                        }

                        if(addedVariants.Count > 0)
                        {
                            await _productVariantRepository.AddARangesync(addedVariants);
                        }

                    }


                    if (product.Files != null && product.Files.Count > 0)
                    {
                        var listProductImage = new List<ProductImage>();
                        foreach (var file in product.Files)
                        {
                            if (file.Length > 0)
                            {
                                ProductImage productImage = new ProductImage();
                                productImage.ProductId = product.Id;
                                productImage.Image = await Utilities.UploadFileAsync(file, "Products");

                                if (product.Image.Equals(file.FileName))
                                {
                                    product.Image = productImage.Image;
                                    productImage.IsDefault = true;
                                }
                                else
                                {
                                    productImage.IsDefault = false;
                                }

                                listProductImage.Add(productImage);
                            }
                        }

                        await _productRepository.AddRangePrioductImageAsync(listProductImage);

                    }

                    _notifyService.Success("Thêm sản phẩm thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            _notifyService.Error("Thêm sản phẩm thất bại");
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var productdetail = await _productRepository.GetDetailProductByIdAsync(id);
            var product = await _productRepository.GetByIdAsync(id);

            if (productdetail == null || product == null)
            {
                return NotFound();
            }

            await InitializeViewData(product);

            var listImages = productdetail?.ProductImages?.ToList();
            var listVariants = productdetail?.Variants?.ToList();

            var productDetailViewModel = new ProductDetailViewModel
            {
                ProductInfo = _mapper.Map<ProductDetailInfoViewModel>(productdetail),
                Images = _mapper.Map<List<ProductImageViewModel>>(listImages),
                Variants = _mapper.Map<List<ProductVariantsViewModel>>(listVariants)
            };

            if (productDetailViewModel.Variants != null)
            {
                var listattributes = await _attributesRepository.GetAllAsync();
                var listattributesvalues = await _attributesValuesRepository.GetAllAsync();

                var attributeDictionary = listattributes.ToDictionary(x => x.Id, x => x.AttributeName);
                var attributeValueDictionary = listattributesvalues.ToDictionary(x => x.Id, x => x.Value);

                foreach (var variant in productDetailViewModel.Variants)
                {
                    if (variant.AttributesJson != null)
                    {
                        try
                        {
                            var objAttributes = JsonConvert.DeserializeObject<List<AttributeJsonToModel>>(variant.AttributesJson);
                            if (objAttributes != null)
                            {
                                foreach (var item in objAttributes)
                                {
                                    var attributeName = attributeDictionary.ContainsKey(item.AttributeName)
                                         ? item.AttributeName
                                         : (int?)null;

                                    var attributeValue = attributeValueDictionary.ContainsKey(item.AttributeValue)
                                        ? item.AttributeValue
                                        : (int?)null;

                                    var ObjecAttributeData = new AttributeData
                                    {
                                        AttributeID = attributeName,
                                        AttributeIDValue = attributeValue,
                                    };

                                    variant.Attributes?.Add(ObjecAttributeData);
                                }
                            }
                        }
                        catch (JsonException ex)
                        {

                            _logger.LogError($"Lỗi parse JSON: {ex.Message}");
                        }


                    }
                }
            }

            return View(productDetailViewModel);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductDetailViewModel productDetailViewModel)
        {
            var productdetailOld = await _productRepository.GetDetailProductByIdAsync(id);

            if (productdetailOld == null)
            {
                return NotFound();
            }
            if (id == null || id != productDetailViewModel.ProductInfo.Id || productDetailViewModel == null || productdetailOld == null)
            {
                return NotFound();
            }

            List<ProductVariantsViewModel> listvariants = new List<ProductVariantsViewModel>();
            if (productDetailViewModel.Variants != null && productDetailViewModel.Variants.Count > 0)
            {
                var listattributes = await _attributesRepository.GetAllAsync();
                var listattributesvalues = await _attributesValuesRepository.GetAllAsync();

                var attributeDictionary = listattributes.ToDictionary(x => x.Id, x => x.AttributeName);
                var attributeValueDictionary = listattributesvalues.ToDictionary(x => x.Id, x => x.Value);


                foreach (var variant in productDetailViewModel.Variants)
                {
                    variant.ProductId = id.Value;

                    if (string.IsNullOrWhiteSpace(variant.AttributesJson) || variant.AttributesJson.Trim() == "[]")
                    {

                        _logger.LogError($"Variant ID: {variant.Id} có AttributesJson rỗng hoặc không hợp lệ.");
                        continue;
                    }

                    if (variant.AttributesJson != null)
                    {
                        try
                        {
                            var objAttributes = JsonConvert.DeserializeObject<List<AttributeJsonToModel>>(variant.AttributesJson);
                            if (objAttributes != null)
                            {
                                foreach (var item in objAttributes)
                                {
                                    var attributeName = attributeDictionary.ContainsKey(item.AttributeName)
                                         ? item.AttributeName
                                         : (int?)null;
                                   
                                    var attributeValue = attributeValueDictionary.ContainsKey(item.AttributeValue)
                                        ? item.AttributeValue
                                        : (int?)null;

                                   
                                    variant.Attributes?.Add(new AttributeData
                                    {
                                        AttributeID = attributeName,
                                        AttributeIDValue = attributeValue,
                                    });
                                }
                            }
                        }
                        catch (JsonException ex)
                        {

                            _logger.LogError($"Lỗi parse JSON: {ex.Message}");
                        }
                    }

                    listvariants.Add(variant);
                }
            }

            var product = _mapper.Map<Product>(productDetailViewModel.ProductInfo);
            var listvariantsmodel = _mapper.Map<List<ProductVariant>>(listvariants);

            productDetailViewModel.Images = _mapper.Map<List<ProductImageViewModel>>(productdetailOld.ProductImages);


            string? DeletedImageIds = productDetailViewModel.ProductInfo.DeletedImageIds;
            List<IFormFile> files = productDetailViewModel.ProductInfo.Files;

            await InitializeViewData(product);

            if (product.ProductCode != productdetailOld?.ProductCode)
            {
                product.ProductCode = productdetailOld?.ProductCode;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // thông tin cơ bản
                    product.Slug = $"{FilterChar.GenerateSlug(product.Title)}-{product.Id}";
                    product.ModifierDate = DateTime.Now;
                    await _productRepository.UpdateAsync(product);

                    // ảnh

                    var currentImages = await _productRepository.GetProductImagesByIdProduct(product.Id);
                    if (DeletedImageIds != null)
                    {
                        List<string> listdeleteidimages = DeletedImageIds.Trim(',').Split(',').ToList();

                        List<int> listDeleteIdImagesInt = listdeleteidimages
                        .Where(id => int.TryParse(id, out _))
                        .Select(int.Parse)
                        .ToList();

                        if (listDeleteIdImagesInt.Count == productdetailOld?.ProductImages?.Count && product.Files.Count == 0)
                        {
                            ModelState.AddModelError("ProductInfo.Image", "Sản phẩm phải có ít nhất một ảnh, không thể xoá hết");
                            return View(productDetailViewModel);
                        }

                        // xoá ảnh
                        if (listDeleteIdImagesInt.Count > 0)
                        {
                            await _productRepository.RemoveRangeProductImageByIDAsync(listDeleteIdImagesInt);
                        }

                    }

                    if (files != null && files.Count > 0)
                    {
                        var listProductImage = new List<ProductImage>();
                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                ProductImage productImage = new ProductImage();
                                productImage.ProductId = product.Id;
                                productImage.Image = await Utilities.UploadFileAsync(file, "Products");

                                if (product.Image.Equals(file.FileName))
                                {
                                    product.Image = productImage.Image;
                                    productImage.IsDefault = true;
                                }
                                else
                                {
                                    productImage.IsDefault = false;
                                }

                                listProductImage.Add(productImage);
                            }
                        }

                        await _productRepository.AddRangePrioductImageAsync(listProductImage);

                    }

                    foreach (var image in currentImages)
                    {
                        image.IsDefault = product.Image == image.Image;
                    }
                    await _productRepository.UpdateRangeImagesAsync(currentImages);

                    // biến thể
                    if (listvariantsmodel.Any())
                    {
                        int currindex = 0;
                        foreach (var vraint in listvariantsmodel)
                        {
                            ++currindex;
                            var existingVariant = await _productVariantRepository.GetBySkuAsync(vraint.SKU);

                            if (existingVariant == null)
                            {
                                vraint.SKU = GenerateSKUVariant(productdetailOld.ProductCode, currindex);
                                vraint.ProductId = product.Id;
                                vraint.IsActive = true;
                                await _productVariantRepository.AddAsync(vraint);
                            }
                            else
                            {
                                existingVariant.Quantity = vraint.Quantity;
                                existingVariant.AdditionalPrice = vraint.AdditionalPrice;
                                existingVariant.AttributesJson = vraint.AttributesJson;
                                existingVariant.IsActive = vraint.IsActive;
                                await _productVariantRepository.UpdateAsync(existingVariant);
                            }
                        }
                    }
                    _notifyService.Success("Cập nhật sản phẩm thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productRepository.ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"Key: {state.Key}");
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
            }

            return View(productDetailViewModel);

        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetDetailProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetDetailProductByIdAsync(id);
            if (product != null)
            {
                await _productRepository.RemoveAsync(product);
            }
 
            return RedirectToAction(nameof(Index));
        }


    }
}
