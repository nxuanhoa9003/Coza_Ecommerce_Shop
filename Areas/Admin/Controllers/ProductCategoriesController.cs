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
using Microsoft.Extensions.Hosting;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using X.PagedList.Extensions;
using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Coza_Ecommerce_Shop.Models.Helper;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryRepository _productcategoryRepository;
        public INotyfService _notifyService { get; }
        private readonly ILogger<ProductCategoriesController> _logger;

        public ProductCategoriesController(IProductCategoryRepository productcategoryRepository, INotyfService notifyService, ILogger<ProductCategoriesController> logger)
        {
            _productcategoryRepository = productcategoryRepository;
            _notifyService = notifyService;
            _logger = logger;
        }

        public async Task<string> HandelSlugProductCategory(string slug, ProductCategory? category = null)
        {
            var listproductcategoryexist = (await _productcategoryRepository.GetByFilterSlugAsNoTrackingAsync(slug)).ToList();

            int count = 0;
            string baseSlug = slug;

            if (category != null)
            {
                listproductcategoryexist.RemoveAll(x => x.Id == category.Id);

            }

            while (listproductcategoryexist.Any(c => c.Slug == slug))
            {
                count++;
                slug = $"{slug}-{count}";
            }

            return slug;

        }


        // GET: Admin/ProductCategories
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var listproductcategories = await _productcategoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                listproductcategories = listproductcategories.Where(x => x.Title.ToLower().Contains(search.ToLower()) || (x.ParentCategory != null && x.ParentCategory.Title.ToLower().Contains(search.ToLower())) || (x.Slug != null && x.Slug.Contains(search)));
            }

            var totalData = listproductcategories.Count();
            var pagedList = listproductcategories.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var productCategoryViewModel = new ProductCategoryViewModel
            {
                ProductCategories = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalData / pageSize),
                    SearchTerm = search,
                }
            };

            return View(productCategoryViewModel);
        }

        // GET: Admin/ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _productcategoryRepository.GetByIdAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Create
        public async Task<IActionResult> Create()
        {
            var categories = (await _productcategoryRepository.GetAllAsync()).ToList();
            ViewBag.Categories = ProductCategoryHelper.BuildCategorySelectList(categories);
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsFeatured,SeoTitle,SeoDescription,SeoKeywords,CreateBy,CreateDate,ModifierDate,ModifiedBy,ParentCategoryId")] ProductCategory productCategory)
        {
            var categories = (await _productcategoryRepository.GetAllAsync()).ToList();
            ViewBag.Categories = ProductCategoryHelper.BuildCategorySelectList(categories);
            try
            {

                if (productCategory.ParentCategoryId.HasValue)
                {
                    if (!int.TryParse(productCategory.ParentCategoryId.ToString(), out int parentId))
                    {
                        productCategory.ParentCategoryId = null;
                    }
                }

                bool isExists = await _productcategoryRepository.IsCategoryExistsAsync(productCategory);

                if (isExists)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại trong cấp hiện tại.");
                    return View(productCategory);
                }

                var slug = FilterChar.GenerateSlug(productCategory.Title);

                slug = await HandelSlugProductCategory(slug);

                productCategory.Slug = slug;
                bool rs = await _productcategoryRepository.AddAsync(productCategory);
                if (rs)
                {
                    _notifyService.Success("Thêm danh mục sản phẩm thành công");
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error("Thêm danh mục sản phẩm thất bại");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _notifyService.Error("Thêm danh mục sản phẩm thất bại");
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _productcategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            var categories = (await _productcategoryRepository.GetAllExceptIdAsync(id)).ToList();
            ViewBag.Categories = ProductCategoryHelper.BuildCategorySelectList(categories);
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsFeatured,SeoTitle,SeoDescription,SeoKeywords,CreateBy,CreateDate,ModifierDate,ModifiedBy,ParentCategoryId")] ProductCategory productCategory)
        {
            var categories = (await _productcategoryRepository.GetAllExceptIdAsync(id)).ToList();
            ViewBag.Categories = ProductCategoryHelper.BuildCategorySelectList(categories);
            
            if (id != productCategory.Id)
            {
                return NotFound();
            }

            if (productCategory.ParentCategoryId.HasValue)
            {
                if (!int.TryParse(productCategory.ParentCategoryId.ToString(), out int parentId))
                {

                    productCategory.ParentCategoryId = null;
                    ModelState.Remove(nameof(productCategory.ParentCategoryId));
                }
            }
            else
            {
                ModelState.Remove(nameof(productCategory.ParentCategoryId));
                productCategory.ParentCategoryId = null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productexist = await _productcategoryRepository.GetByIdAsNoTrackingAsync(id);
                    if (productexist == null)
                    {
                        return NotFound();
                    }

                    bool isExists = await _productcategoryRepository.IsCategoryExistsAsync(productCategory, productexist);

                    if (isExists)
                    {
                        ModelState.AddModelError("", "Danh mục đã tồn tại trong cấp hiện tại.");
                        return View(productCategory);
                    }


                    productCategory.CreateDate = productexist.CreateDate;

                    var slug = FilterChar.GenerateSlug(productCategory.Title);
                    slug = await HandelSlugProductCategory(slug, productexist);

                    productCategory.Slug = slug;
                    bool rs = await _productcategoryRepository.UpdateAsync(productCategory);

                    if (rs)
                    {
                        _notifyService.Success("Cập nhật danh mục sản phẩm thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    _notifyService.Error("Cập nhật danh mục sản phẩm thất bại");

                }
                catch (DbUpdateConcurrencyException)
                {
                    var productcategoryfind = await _productcategoryRepository.GetByIdAsync(id);
                    if (productcategoryfind == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }


        // GET: Admin/ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _productcategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = await _productcategoryRepository.GetByIdAsync(id);
            if (productCategory != null)
            {
                var rs = await _productcategoryRepository.RemoveAsync(productCategory);
                if(rs)
                {
                    _notifyService.Success("Xoá danh mục thành công");
                }else
                {
                    _notifyService.Error("Xoá danh mục thất bại");
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
