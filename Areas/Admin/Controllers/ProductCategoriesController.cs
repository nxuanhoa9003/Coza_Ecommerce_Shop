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

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryRepository _productcategoryRepository;
        public INotyfService _notifyService { get; }

        public ProductCategoriesController(IProductCategoryRepository productcategoryRepository, INotyfService notifyService)
        {
            _productcategoryRepository = productcategoryRepository;
            _notifyService = notifyService;
        }

        // GET: Admin/ProductCategories
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var listproductcategories = await _productcategoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                listproductcategories = listproductcategories.Where(x => x.Title.Contains(search) || (x.Slug != null && x.Slug.Contains(search)));
            }

            var totalProductCategory = listproductcategories.Count();
            var pagedList = listproductcategories.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var productCategoryViewModel = new ProductCategoryViewModel
            {
                ProductCategories = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalProductCategory / pageSize),
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,SeoTitle,SeoDescription,SeoKeywords,CreateBy,CreateDate,ModifierDate,ModifiedBy")] ProductCategory productCategory, IFormFile file)
        {

            try
            {
                productCategory.CreateDate = DateTime.Now;
                productCategory.ModifierDate = DateTime.Now;
                productCategory.Slug = FilterChar.GenerateSlug(productCategory.Title);
                await _productcategoryRepository.AddAsync(productCategory);
                _notifyService.Success("Thêm tin tức thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,SeoTitle,SeoDescription,SeoKeywords,CreateBy,CreateDate,ModifierDate,ModifiedBy")] ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productcategoryRepository.UpdateAsync(productCategory);
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
                return RedirectToAction(nameof(Index));
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
                await _productcategoryRepository.RemoveAsync(productCategory);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
