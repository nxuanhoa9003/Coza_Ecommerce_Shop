using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Banner;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class BannerController : Controller
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IMapper _mapper;
        public INotyfService _notifyService { get; }

        public BannerController(IBannerRepository bannerRepository, IMapper mapper, INotyfService notifyService)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = await _bannerRepository.GetBannersAsync();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.ToLower()));
            }

            var totalData = query.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;


            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            var pagedList = query.OrderByDescending(x => x.CreateDate).ToPagedList(pageNumber, pageSize);

            var bannerPagingViewModel = new BannerPagingViewModel
            {
                Banners = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };
            return View(bannerPagingViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CreateBanner")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateBanner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;

                var UrlImage = await Utilities.UploadFileAsync(model.file, "Banner");
                if (string.IsNullOrEmpty(UrlImage))
                {
                    _notifyService.Error("Tạo banner thất bại,hãy thử lại");
                    return View(model);
                }
                var banner = new Banner
                {
                    Title = model.Title,
                    Description = model.Description,
                    Image = UrlImage,
                    Link = model.Link,
                    Type = model.Type,
                    IsShow = true,
                    CreateBy = fullName
                };

                var (IsSuccess, Errormessage) = await _bannerRepository.CreateAsync(banner);
                if (IsSuccess)
                {
                    _notifyService.Success(Errormessage);
                    return RedirectToAction("Index");

                }
                Utilities.DeleteImage(UrlImage);
                _notifyService.Error(Errormessage);
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Policy = "EditBanner")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notifyService.Warning("Không tìm thấy banner nào hợp lệ");
                return RedirectToAction("Index");
            }
            var banner = await _bannerRepository.GetByIdAsync(id);
            var bannerViewModel = _mapper.Map<BannerViewModel>(banner);
            return View(bannerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditBanner")]
        public async Task<IActionResult> Edit(string id, [FromForm] BannerViewModel model)
        {
            if (string.IsNullOrEmpty(id) || Guid.Parse(id) != model.Id)
            {
                _notifyService.Warning("Không tìm thấy banner nào hợp lệ");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
                var bannerOld = await _bannerRepository.GetByIdAsync(id);
                if (bannerOld == null)
                {
                    _notifyService.Warning("Cập nhật thất bại");
                    return View(model);
                }
                var imageOld = bannerOld.Image;


                bannerOld.ModifierDate = DateTime.Now;
                bannerOld.ModifiedBy = fullName;

                if (model.file != null && model.file.Length > 0)
                {
                    var ImageUrlNew = await Utilities.UploadFileAsync(model.file, "Banner");
                    Utilities.DeleteImage(bannerOld.Image); // Xóa ảnh cũ
                    bannerOld.Image = ImageUrlNew;
                }

                var (IsSuccess, Errormessage) = await _bannerRepository.UpdateAsync(bannerOld);
                if (IsSuccess)
                {
                    if (model.file != null && model.file.Length > 0)
                    {
                        Utilities.DeleteImage(imageOld);
                    }
                    _notifyService.Success(Errormessage);
                    return RedirectToAction("Index");
                }
                else
                {
                    _notifyService.Error(Errormessage);
                }

            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notifyService.Warning("Không tìm thấy banner nào hợp lệ");
                return RedirectToAction("Index");
            }
            var banner = await _bannerRepository.GetByIdAsync(id);
            return View(banner);
        }


        [HttpPost("Delete/{id}")]
        [Authorize(Policy = "DeleteBanner")]

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notifyService.Warning("Không tìm thấy banner nào hợp lệ");
                return RedirectToAction("Index");
            }
            var banner = await _bannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                _notifyService.Warning("Không tìm thấy banner nào hợp lệ");
                return RedirectToAction("Index");
            }
            var image = banner.Image;

            var (IsSuccess, ErrorMessage) = await _bannerRepository.DeleteAsync(banner);
            if (IsSuccess)
            {
                Utilities.DeleteImage(image);
                _notifyService.Success(ErrorMessage);
                return RedirectToAction("Index");

            }
            _notifyService.Error(ErrorMessage);
            return RedirectToAction("Index");
        }
    }
}
