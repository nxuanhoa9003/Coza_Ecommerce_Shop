using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Models.Common;
using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public INotyfService _notifyService { get; }
        public CategoryController(ICategoryRepository categoryRepository, INotyfService notifyService)
        {
            _categoryRepository = categoryRepository;
            _notifyService = notifyService;
        }

        // GET: Admin/Category
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listcategories = await _categoryRepository.GetAllAsync();
            return View(listcategories);
        }

        // GET: Admin/Category/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Category/Create
        [HttpGet]
        [Authorize(Policy = "CreateCategory")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateCategory")]
        public async Task<IActionResult> Create([Bind("Title,Description,Position,SeoTitle,SeoDescription,SeoKeywords")] Category category)
        {

            var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;

            if (ModelState.IsValid)
            {

                category.CreateDate = DateTime.Now;
                category.ModifierDate = DateTime.Now;
                category.Slug = FilterChar.GenerateSlug(category.Title);
                category.CreateBy = fullName;
                await _categoryRepository.AddAsync(category);
                _notifyService.Success("Thêm danh mục thành công");
                return RedirectToAction(nameof(Index));
            }
            
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        [HttpGet]
        [Authorize(Policy = "EditCategory")]

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditCategory")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Position,SeoTitle,SeoDescription,SeoKeywords")] Category category)
        {

            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
                    var existingCategory = await _categoryRepository.GetByIdAsync(id);

                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    existingCategory.Title = category.Title;
                    existingCategory.Description = category.Description;
                    existingCategory.Position = category.Position;
                    existingCategory.SeoTitle = category.SeoTitle;
                    existingCategory.SeoDescription = category.SeoDescription;
                    existingCategory.SeoKeywords = category.SeoKeywords;
                    existingCategory.CreateDate = existingCategory.CreateDate;
                    existingCategory.CreateBy = existingCategory.CreateBy;
                    existingCategory.ModifierDate = DateTime.Now;
                    existingCategory.ModifiedBy = fullName;
                    existingCategory.Slug = FilterChar.GenerateSlug(category.Title);

                    await _categoryRepository.UpdateAsync(existingCategory);

                    _notifyService.Success("Cập nhật danh mục thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    var catgoryfind = await _categoryRepository.GetByIdAsync(id);
                    if (catgoryfind == null)
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
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        [HttpGet]
        [Authorize(Policy = "DeleteCategory")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteCategory")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
               await _categoryRepository.RemoveAsync(category);
            }

            _notifyService.Success("Xoá danh mục thành công");
            return RedirectToAction(nameof(Index));
        }

        
    }
}
