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
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        public INotyfService _notifyService { get; }

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository, INotyfService notifyService)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _notifyService = notifyService;
        }

        // GET: Admin/Posts
        [HttpGet]   
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 2;
            int pageNumber = page ?? 1;

            var query = await _postRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search) || (x.Slug != null && x.Slug.Contains(search)));
            }

            var totalData = query.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;

            var pagedList = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var PostViewModel = new PostViewModel
            {
                Posts = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };

            return View(PostViewModel);
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        [HttpGet]
        [Authorize(Policy = "CreatePost")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreatePost")]
        public async Task<IActionResult> Create([Bind("Title,Description,Detail,Image,CategoryId,SeoTitile,SeoDescription,SeoKeywords")] Post post, IFormFile file)
        {
            if(file == null)
            {
                ModelState.AddModelError("Image", "Chưa chọn ảnh.");
                return View(post);
            }

            if (ModelState.IsValid)
            {
                var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;

                try
                {
                    post.IsActive = true;
                    post.CreateDate = DateTime.Now;
                    post.CreateBy = fullName;
                    post.ModifierDate = DateTime.Now;
                    post.Slug = FilterChar.GenerateSlug(post.Title);
                    post.Image = await Utilities.UploadFileAsync(file, "Posts");
                    post.Category = await _categoryRepository.GetByIdAsync(post.CategoryId);

                    await _postRepository.AddAsync(post);

                    _notifyService.Success("Thêm tin tức thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Title", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        [HttpGet]
        [Authorize(Policy = "EditPost")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Title", post.CategoryId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPost")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Slug,Description,Detail,Image,CategoryId,SeoTitile,SeoDescription,SeoKeywords,IsActive")] Post post, IFormFile file)
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
                var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
                try
                {
                    var existingPost = await _postRepository.GetByIdAsync(id);
                    if (existingPost == null)
                    {
                        return NotFound();
                    }

                    existingPost.ModifierDate = DateTime.Now;
                    existingPost.ModifiedBy = fullName;
                    existingPost.CreateDate = existingPost.CreateDate;
                    existingPost.CreateBy = existingPost.CreateBy;
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

                    await _postRepository.UpdateAsync(existingPost);

                    _notifyService.Success("Cập nhập tin tức thành công");

                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Title", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        [HttpGet]
        [Authorize(Policy = "DeletePost")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePost")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                Utilities.DeleteImage(post.Image);
                await _postRepository.RemoveAsync(post);
            }

            return RedirectToAction(nameof(Index));
        }


        // Post: delete select
        [HttpPost]
        [Authorize(Policy = "DeletePost")]
        public async Task<IActionResult> DeleteNewsSelect([FromBody] List<Guid> ids)
        {
            if (!ids.IsNullOrEmpty())
            {
                foreach (var id in ids)
                {
                    var objpost = await _postRepository.GetByIdAsync(id);
                    if (objpost != null)
                    {
                        Utilities.DeleteImage(objpost.Image);
                        await _postRepository.RemoveAsync(objpost);
                    }

                }
                _notifyService.Success("Xoá thành công");
                return Ok(new { success = true });
            }

            return Ok(new { success = false });
        }
    }
}
