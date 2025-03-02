using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using X.PagedList.Extensions;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly INewRepository _newRepository;

        public NewsController(INewRepository newRepository)
        {
            _newRepository = newRepository;
        }

        [HttpGet("")]
        [HttpGet("[action]")]
        public async Task<IActionResult> Index (int? page = 1)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var query = await _newRepository.GetAllAsync();
            var totalData = query.Count();

            var totalPages = (int)Math.Ceiling((double)totalData / pageSize);

            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            var pagedList = query.OrderByDescending(x => x.CreateDate).ToPagedList(pageNumber, pageSize);


            var NewsViewModel = new NewsViewModel
            {
                News = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                }
            };
            return View(NewsViewModel);
        }


        [HttpGet("detail/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound("Không tìm thấy tin tức");
            }

            var model = await _newRepository.GetBySlugAsync(slug);
            return View(model);

        }
    }
}
