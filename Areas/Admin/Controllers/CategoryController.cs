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

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public INotyfService _notifyService { get; }
        public CategoryController(AppDbContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index()
        {
            var data = await _context.Categories.ToListAsync();
            return View(data);
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Position,SeoTitle,SeoDescription,SeoKeywords")] Category category)
        {

            if (ModelState.IsValid)
            {
                category.CreateDate = DateTime.Now;
                category.ModifierDate = DateTime.Now;
                category.Slug = FilterChar.GenerateSlug(category.Title);
                _context.Add(category);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm danh mục thành công");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        string propertyName = entry.Key; // Tên thuộc tính
                        var errors = entry.Value.Errors.Select(e => e.ErrorMessage).ToList(); // Danh sách lỗi
                        Console.WriteLine($"Thuộc tính {propertyName} có lỗi: {string.Join(", ", errors)}");
                    }
                }
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Position,SeoTitle,SeoDescription,SeoKeywords")] Category category)
        {

            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = await _context.Categories.FindAsync(id);

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

                    existingCategory.ModifierDate = DateTime.Now;

                    existingCategory.Slug = FilterChar.GenerateSlug(category.Title);
                    
                    _context.Update(existingCategory);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật danh mục thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            _notifyService.Information("Xoá danh mục thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
