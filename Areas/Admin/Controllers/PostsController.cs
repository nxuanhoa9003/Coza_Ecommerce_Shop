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
using Microsoft.IdentityModel.Tokens;
using X.PagedList.Extensions;
using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.Extensions.Hosting;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;
        public INotyfService _notifyService { get; }

        public PostsController(AppDbContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 2;
            int pageNumber = page ?? 1;

            var query = _context.Posts.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search) || (x.Slug != null && x.Slug.Contains(search)));
            }

            var totalPosts = query.Count();

            var pagedList = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);



            var PostViewModel = new PostViewModel
            {
                Posts = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize),
                    SearchTerm = search,
                }


            };

            return View(PostViewModel);
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Detail,Image,CategoryId,SeoTitile,SeoDescription,SeoKeywords")] Post post, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    post.IsActive = true;
                    post.CreateDate = DateTime.Now;
                    post.ModifierDate = DateTime.Now;
                    post.Slug = FilterChar.GenerateSlug(post.Title);
                    post.Image = await Utilities.UploadFileAsync(file, "posts");
                    post.Category = _context.Categories.Find(post.CategoryId);
                    _context.Add(post);
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", post.CategoryId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Slug,Description,Detail,Image,CategoryId,SeoTitile,SeoDescription,SeoKeywords,IsActive")] Post post, IFormFile file)
        {
            if (id != post.Id)
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
                    var existingPost = await _context.Posts.FindAsync(id);
                    if (existingPost == null)
                    {
                        return NotFound();
                    }

                    existingPost.ModifierDate = DateTime.Now;
                    existingPost.CreateDate = existingPost.CreateDate;

                    existingPost.Title = post.Title;
                    existingPost.Detail = post.Detail;
                    existingPost.Description = post.Description;
                    existingPost.SeoTitile = post.SeoTitile;
                    existingPost.SeoKeywords = post.SeoKeywords;
                    existingPost.SeoDescription = post.SeoDescription;
                    existingPost.IsActive = post.IsActive;
                    existingPost.Slug = FilterChar.GenerateSlug(post.Title);

                    if (file != null)
                    {
                        Utilities.DeleteImage(existingPost.Image);
                        existingPost.Image = await Utilities.UploadFileAsync(file, "posts");

                    }
                    else
                    {
                        existingPost.Image = existingPost.Image;

                    }

                    existingPost.Category = _context.Categories.Find(post.CategoryId);

                    _context.Update(existingPost);
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                Utilities.DeleteImage(post.Image);
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
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
                    var objpost = _context.Posts.Find(id);
                    if (objpost != null)
                    {
                        Utilities.DeleteImage(objpost.Image);
                        _context.Posts.Remove(objpost);
                    }

                }
                _context.SaveChanges();
                _notifyService.Success("Xoá thành công");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
