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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;
using X.PagedList.Extensions;
using Coza_Ecommerce_Shop.ViewModels;



namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        public INotyfService _notifyService { get; }

        public NewsController(AppDbContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            //var appDbContext = await _context.News.Include(x => x.Category).OrderByDescending(x => x.Id).ToListAsync();
            var query = _context.News.Include(x => x.Category).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search) || (x.Slug != null && x.Slug.Contains(search)));
            }

            var totalPosts = await query.CountAsync();

            var pagedList = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var NewsViewModel = new NewsViewModel
            {
                News = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize),
                    SearchTerm = search,
                }
            };

            return View(NewsViewModel);
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(x => x.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Detail,Image,CategoryId,SeoTitile,SeoDescription,SeoKeywords")] New news, IFormFile file)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    news.IsActive = true;
                    news.CreateDate = DateTime.Now;
                    news.ModifierDate = DateTime.Now;
                    news.Slug = FilterChar.GenerateSlug(news.Title);
                    news.Image = await Utilities.UploadFileAsync(file, "News");
                    news.Category = _context.Categories.Find(news.CategoryId);
                    _context.Add(news);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Thêm tin tức thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


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

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", news.CategoryId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", news.CategoryId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Slug,Description,Detail,CategoryId,SeoTitile,SeoDescription,SeoKeywords,CreateBy,CreateDate,ModifierDate,ModifiedBy,IsActive")] New news, IFormFile file)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (file is null)
            {
                ModelState.Remove("file");
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var existingNew = await _context.News.FindAsync(id);
                    if (existingNew == null)
                    {
                        return NotFound();
                    }

                    existingNew.ModifierDate = DateTime.Now;
                    existingNew.CreateDate = existingNew.CreateDate;

                    existingNew.Title = news.Title;
                    existingNew.Detail = news.Detail;
                    existingNew.Description = news.Description;
                    existingNew.SeoTitile = news.SeoTitile;
                    existingNew.SeoKeywords = news.SeoKeywords;
                    existingNew.SeoDescription = news.SeoDescription;
                    existingNew.IsActive = news.IsActive;
                    existingNew.Slug = FilterChar.GenerateSlug(news.Title);

                    if (file != null)
                    {
                        Utilities.DeleteImage(existingNew.Image);
                        existingNew.Image = await Utilities.UploadFileAsync(file, "News");
                    }
                    else
                    {
                        existingNew.Image = existingNew.Image;

                    }

                    existingNew.Category = _context.Categories.Find(news.CategoryId);

                    _context.Update(existingNew);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhập tin tức thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", news.CategoryId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(x => x.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                Utilities.DeleteImage(news.Image);
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            _notifyService.Information("Xoá tin tức thành công");

            return RedirectToAction(nameof(Index));
        }


        // Post: delete select
        [HttpPost]
        public ActionResult DeleteNewsSelect([FromBody] List<int> ids)
        {
            if (!ids.IsNullOrEmpty())
            {
                foreach (var id in ids)
                {
                    var objnews = _context.News.Find(id);
                    if (objnews != null)
                    {
                        Utilities.DeleteImage(objnews.Image);
                        _context.News.Remove(objnews);
                    }

                }
                _context.SaveChanges();
                _notifyService.Success("Xoá thành công");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        private bool NewExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
