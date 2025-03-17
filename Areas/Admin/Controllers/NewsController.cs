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
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;



namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class NewsController : Controller
    {
        private readonly INewRepository _newRepository;
        private readonly ICategoryRepository _categoryRepository;

        public INotyfService _notifyService { get; }

        public NewsController(INewRepository newRepository, ICategoryRepository categoryRepository, INotyfService notifyService)
        {
           
            _newRepository = newRepository;
            _categoryRepository = categoryRepository;
            _notifyService = notifyService;
        }

        // GET: Admin/News
        [HttpGet]
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = await _newRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search) || (x.Slug != null && x.Slug.Contains(search)));
            }

            var totalData = query.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;


            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            var pagedList = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var NewsViewModel = new NewsViewModel
            {
                News = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };

            return View(NewsViewModel);
        }

        // GET: Admin/News/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        [HttpGet]
        [Authorize(Policy = "CreateNews")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateNews")]
        public async Task<IActionResult> Create([Bind("Title,Description,Detail,Image,CategoryId,SeoTitile,SeoDescription,SeoKeywords")] New news, IFormFile file)
        {
            if (file == null)
            {
                ModelState.AddModelError("Image", "Chưa chọn ảnh.");
                return View(news);
            }
            if (ModelState.IsValid)
            {
                var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;

                try
                {
                    news.IsActive = true;
                    news.CreateBy = fullName;
                    news.CreateDate = DateTime.Now;
                    news.ModifierDate = DateTime.Now;
                    news.Slug = FilterChar.GenerateSlug(news.Title);
                    news.Image = await Utilities.UploadFileAsync(file, "News");
                    news.Category = await _categoryRepository.GetByIdAsync(news.CategoryId);

                    await _newRepository.AddAsync(news);
                    _notifyService.Success("Thêm tin tức thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            
            var listcategory = await _categoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(listcategory, "Id", "Title", news.CategoryId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        [HttpGet]
        [Authorize(Policy = "EditNews")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            var listcategory = await _categoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(listcategory, "Id", "Title", news.CategoryId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditNews")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Slug,Description,Detail,CategoryId,SeoTitile,SeoDescription,SeoKeywords,CreateBy,CreateDate,ModifierDate,ModifiedBy,IsActive")] New news, IFormFile file)
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
                    var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;

                    var existingNew = await _newRepository.GetByIdAsync(id);
                    if (existingNew == null)
                    {
                        return NotFound();
                    }

                    existingNew.ModifierDate = DateTime.Now;
                    existingNew.ModifiedBy = fullName;
                    existingNew.CreateDate = existingNew.CreateDate;
                    existingNew.CreateBy = existingNew.CreateBy;

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

                   
                    await _newRepository.UpdateAsync(existingNew);

                    _notifyService.Success("Cập nhập tin tức thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }

            var listcategory = await _categoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(listcategory, "Id", "Title", news.CategoryId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        [HttpGet]
        [Authorize(Policy = "DeleteNews")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteNews")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var news = await _newRepository.GetByIdAsync(id);
            if (news != null)
            {
                Utilities.DeleteImage(news.Image);
                await _newRepository.RemoveAsync(news);
            }

            _notifyService.Success("Xoá tin tức thành công");

            return RedirectToAction(nameof(Index));
        }


        // Post: delete select
        [HttpPost]
        [Authorize(Policy = "DeleteNews")]
        public async Task<IActionResult> DeleteNewsSelect([FromBody] List<Guid> ids)
        {
            if (!ids.IsNullOrEmpty())
            {
                foreach (var id in ids)
                {
                    var objnews = await _newRepository.GetByIdAsync(id);
                    if (objnews != null)
                    {
                        Utilities.DeleteImage(objnews.Image);
                        await _newRepository.RemoveAsync(objnews);
                    }
                }
                _notifyService.Success("Xoá thành công");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

    }
}
