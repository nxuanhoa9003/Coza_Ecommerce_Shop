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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using X.PagedList.Extensions;
using Coza_Ecommerce_Shop.Models.Helper;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using NuGet.Packaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.Authorization;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        private readonly IProductVariantRepository _productVariantRepository;

        private readonly ILogger<ProductsController> _logger;
        public INotyfService _notifyService { get; }

        public ProductsController
        (

            ILogger<ProductsController> logger,
            IMapper mapper, IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,

            IProductVariantRepository productVariantRepository,
            INotyfService notifyService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productVariantRepository = productVariantRepository;
            _notifyService = notifyService;
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

        // GET: Admin/Products
        [HttpGet]
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            var listproducts = await _productRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                listproducts = listproducts.Where(x => x.Title.ToLower().Contains(search.ToLower()) || (x.ProductCode != null && x.ProductCode.ToLower().Contains(search.ToLower())));
            }


            int pageSize = 5;
            int pageNumber = page ?? 1;
            var totalData = listproducts.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;


            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            var pagedList = listproducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);


            var ProductsPagingViewModel = new ProductsPagingViewModel
            {
                Products = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };

            return View(ProductsPagingViewModel);
        }

        // GET: Admin/Products/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
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
            return View(productdetail);
        }

        private bool ValidateCreateProductViewModel(ProductViewModel productViewModel)
        {

            if (productViewModel.Variants.Any(x => x.BasePrice == null || x.Quantity == null))
            {
                ModelState.AddModelError("", "Chưa nhập đủ thông tin cho các biến thể của sản phẩm.");
                return false;
            }

            if (productViewModel.Files == null || productViewModel.Files.Count == 0)
            {
                ModelState.AddModelError("Files", "Sản phẩm có ít nhất 1 ảnh.");
                return false;
            }

            if (productViewModel.Variants == null || !productViewModel.Variants.Any())
            {
                ModelState.AddModelError("", "Sản phẩm có ít nhất 1 biến thể.");
                return false;
            }

            var duplicateVariants = productViewModel.Variants
                .GroupBy(v => new { v.Color, v.Size }) // Nhóm theo Color và Size
                .Where(g => g.Count() > 1)
                 .SelectMany(g => g.Skip(1))
                .ToList();

            if (duplicateVariants.Any())
            {
                string strerr = "";
                foreach (var variant in duplicateVariants)
                {
                    strerr += $"Biến thể bị trùng: Màu {variant.Color}, Size {variant.Size}";
                }

                ModelState.AddModelError("", strerr);
                return false;
            }

            if (!ProcessProductImages(productViewModel))
            {
                return false;
            }

            return true;
        }

        private bool ValidateEditProductViewModel(ProductViewModel productViewModel)
        {
            if (productViewModel.Variants.Any(x => x.Color == null || x.Size == null || x.BasePrice == null || x.Quantity == null))
            {
                ModelState.AddModelError("ErrorVariant", "Chưa nhập đủ thông tin cho các biến thể của sản phẩm.");
                return false;
            }

            var duplicateVariants = productViewModel.Variants
               .GroupBy(v => new { v.Color, v.Size })
               .Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1))
               .ToList();

            if (duplicateVariants.Any())
            {
                string strerr = "";
                foreach (var variant in duplicateVariants)
                {
                    strerr += $"Biến thể bị trùng: Màu {variant.Color}, Size {variant.Size}";
                }

                ModelState.AddModelError("ErrorVariant", strerr);
                return false;
            }

            return true;
        }

        private bool ProcessProductImages(ProductViewModel productViewModel)
        {
            foreach (var file in productViewModel.Files)
            {
                if (file.Length > 8 * 1024 * 1024) // 8MB
                {
                    ModelState.AddModelError("Files", "File không được lớn hơn 8MB.");
                    return false;
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ModelState.AddModelError("Files", "Chỉ hỗ trợ định dạng JPG, PNG, JPEG, GIF.");
                    return false;
                }
            }
            return true;
        }

        private void ProcessVariants(ProductViewModel productViewModel, Product product)
        {
            int variantNumber = 0;
            var existingVariants = product.Variants.ToList();
            var listsku = existingVariants.Select(x => x.SKU).ToList();
            foreach (var item in productViewModel.Variants)
            {

                var existingVariant = existingVariants.FirstOrDefault(v => v.Id == item.Id);
                if (existingVariant == null)
                {
                    item.Id ??= Guid.NewGuid();
                    var newsku = GenerateSKUVariant(productViewModel.ProductCode, ++variantNumber);
                    while (listsku.Contains(newsku))
                    {
                        newsku = GenerateSKUVariant(productViewModel.ProductCode, ++variantNumber);
                    }
                    item.SKU = newsku;
                    item.SKU = GenerateSKUVariant(productViewModel.ProductCode, ++variantNumber);
                    item.ProductId = product.Id;
                    item.BasePrice ??= 0;
                    item.PriceSale ??= 0;
                    item.Quantity ??= 0;
                    item.IsActive = true;

                    product.Variants.Add(new ProductVariant
                    {
                        Id = item.Id.Value,
                        ProductId = product.Id,
                        SKU = item.SKU,
                        Color = item.Color,
                        Size = item.Size,
                        BasePrice = item.BasePrice.Value,
                        PriceSale = item.PriceSale.Value,
                        Quantity = item.Quantity.Value,
                        IsActive = item.IsActive,
                        IsDefault = item.IsDefault
                    });
                }
                else
                {
                    // Cập nhật biến thể đã tồn tại
                    existingVariant.SKU = existingVariant.SKU;
                    existingVariant.Color = item.Color;
                    existingVariant.Size = item.Size;
                    existingVariant.BasePrice = item.BasePrice ?? 0;
                    existingVariant.PriceSale = item.PriceSale ?? 0;
                    existingVariant.Quantity = item.Quantity ?? 0;
                    existingVariant.IsActive = item.IsActive;
                    existingVariant.IsDefault = item.IsDefault;
                }

            }
            if (product.Variants.All(x => !x.IsDefault))
            {
                product.Variants.First().IsDefault = true;
            }

        }

        private async Task ProcessProductImages(ProductViewModel productViewModel, Product product)
        {

            // Xóa ảnh cũ nếu có danh sách ID ảnh cần xóa
            if (!string.IsNullOrEmpty(productViewModel.DeletedImageIds))
            {
                var deletedIds = productViewModel.DeletedImageIds
                                .Split(',')
                                .Where(x => !string.IsNullOrWhiteSpace(x))
                                .Select(Guid.Parse)
                                .ToList();
                var listproductImages = product.ProductImages.Where(x => deletedIds.Contains(x.Id)).ToList();
                if (listproductImages.Count > 0)
                {
                    foreach (var item in listproductImages)
                    {
                        product.ProductImages.Remove(item);
                    }
                }

            }



            if (productViewModel.Files != null && productViewModel.Files.Count > 0)
            {
                var listProductImages = new List<ProductImage>();

                foreach (var file in productViewModel.Files)
                {
                    if (file.Length > 0)
                    {

                        var productImage = new ProductImage
                        {
                            ProductId = product.Id,
                            Image = await Utilities.UploadFileAsync(file, "Products"),
                            IsDefault = productViewModel.Image == file.FileName

                        };

                        if (productViewModel.Image == file.FileName)
                        {
                            product.Image = productImage.Image;
                            productViewModel.Image = productImage.Image;
                        }



                        listProductImages.Add(productImage);
                    }
                }
                product.ProductImages.AddRange(listProductImages);
            }

            foreach (var image in product.ProductImages)
            {

                if (productViewModel.Image == image.Image)
                {
                    image.IsDefault = true;
                    product.Image = image.Image;
                }
                else
                {
                    image.IsDefault = false;

                }
            }

        }


        private void UpdateBaseInfoProduct(ProductViewModel productViewModel, Product product)
        {
            product.Title = productViewModel.Title;
            product.Description = productViewModel.Description;
            product.Detail = productViewModel.Detail;
            product.ProductCategoryId = productViewModel.ProductCategoryId;
            product.IsHome = productViewModel.IsHome;
            product.IsHot = productViewModel.IsHot;
            product.IsFeature = productViewModel.IsFeature;
            product.IsSale = productViewModel.IsSale;
            product.IsActive = productViewModel.IsActive;
            product.IsDeleted = productViewModel.IsDeleted;
            product.SeoTitile = productViewModel.SeoTitile;
            product.SeoDescription = productViewModel.SeoDescription;
            product.SeoKeywords = productViewModel.SeoKeywords;
            string titleSlug = string.IsNullOrWhiteSpace(productViewModel.Title) ? "san-pham" : FilterChar.GenerateSlug(productViewModel.Title);
            product.Slug = $"{titleSlug}-{product.Id.ToString("N")}";

            product.Quantity = productViewModel.Variants.Sum(x => x.Quantity);
        }

        // GET: Admin/Products/Create
        [HttpGet]
        [Authorize(Policy = "CreateProduct")]
        public async Task<IActionResult> Create()
        {
            var categories = (await _productCategoryRepository.GetAllAsync()).ToList();
            ViewData["ProductCategoryId"] = ProductCategoryHelper.BuildCategorySelectList(categories);
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateProduct")]
        public async Task<IActionResult> Create([FromForm] ProductViewModel productViewModel)
        {
            var categories = (await _productCategoryRepository.GetAllAsync()).ToList();
            ViewData["ProductCategoryId"] = ProductCategoryHelper.BuildCategorySelectList(categories);

            if (!ValidateCreateProductViewModel(productViewModel))
            {
                return View(productViewModel);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    ProcessVariants(productViewModel, new Product());
                    var product = _mapper.Map<Product>(productViewModel);

                    if (string.IsNullOrEmpty(product.ProductCode))
                    {
                        product.ProductCode = GenerateProductSKU();
                    }
                    else
                    {
                        if (await _productRepository.IsDuplicateProductCode(product))
                        {
                            ModelState.AddModelError("ProductCode", "Mã SKU của sản phẩm đã tồn tại.");
                            return View(productViewModel);
                        }
                    }

                    product.Quantity = productViewModel.Variants.Sum(x => x.Quantity);
                    product.IsActive = true;
                    product.CreateDate = DateTime.Now;
                    product.ModifierDate = DateTime.Now;
                    string titleSlug = string.IsNullOrWhiteSpace(product.Title) ? "san-pham" : FilterChar.GenerateSlug(product.Title);
                    product.Slug = $"{titleSlug}-{product.Id.ToString("N")}";

                    if (productViewModel.Files.Any())
                    {
                        await ProcessProductImages(productViewModel, product);
                    }

                    var rs = await _productRepository.AddAsync(product);
                    if (rs)
                    {
                        _notifyService.Success("Thêm sản phẩm thành công");
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            LogModelStateErrors();
            _notifyService.Error("Thêm sản phẩm thất bại");
            return View(productViewModel);
        }

        // GET: Admin/Products/Edit/5
        [HttpGet]
        [Authorize(Policy = "EditProduct")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var categories = (await _productCategoryRepository.GetAllAsync()).ToList();
            ViewData["ProductCategoryId"] = ProductCategoryHelper.BuildCategorySelectList(categories);

            if (id == null)
            {
                return NotFound();
            }

            var productdetail = await _productRepository.GetDetailProductByIdAsync(id);

            if (productdetail == null)
            {
                return NotFound();
            }
            var ProductViewModel = _mapper.Map<ProductViewModel>(productdetail);

            return View(ProductViewModel);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditProduct")]
        public async Task<IActionResult> Edit(Guid? id, ProductViewModel productDetailViewModel)
        {
            var categories = (await _productCategoryRepository.GetAllAsync()).ToList();
            ViewData["ProductCategoryId"] = ProductCategoryHelper.BuildCategorySelectList(categories);
            var productOld = await _productRepository.GetDetailProductByIdAsync(id);

            if (productOld == null)
            {
                return NotFound();
            }
            if (id == null || id != productDetailViewModel.Id || productDetailViewModel == null || productOld == null)
            {
                return NotFound();
            }


            productDetailViewModel.ProductCode = productOld.ProductCode;

            if (!ValidateEditProductViewModel(productDetailViewModel))
            {
                return View(productDetailViewModel);
            }

            var deletedIds = productDetailViewModel?.DeletedImageIds?.Split(',')
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(Guid.Parse).ToList();

            if (deletedIds != null && deletedIds.Count == productOld.ProductImages.Count)
            {
                ModelState.AddModelError("Image", "Sản phẩm phải có ít nhất 1 ảnh.");
                return View(productDetailViewModel);
            }


            try
            {
                UpdateBaseInfoProduct(productDetailViewModel, productOld);

                await ProcessProductImages(productDetailViewModel, productOld);

                ProcessVariants(productDetailViewModel, productOld);


                await _productRepository.UpdateAsync(productOld);
                _notifyService.Success("Cập nhật sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            _notifyService.Error("Cập nhật sản phẩm thất bại");
            return View(productDetailViewModel);

        }

        // GET: Admin/Products/Delete/5
        [HttpGet]
        [Authorize(Policy = "DeleteProduct")]
        public async Task<IActionResult> Delete(Guid? id)
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
        [Authorize(Policy = "DeleteProduct")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productRepository.GetDetailProductByIdAsync(id);
            if (product != null)
            {
                await _productRepository.RemoveAsync(product);
            }
            return RedirectToAction(nameof(Index));
        }

        private void LogModelStateErrors()
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

    }
}
