using Coza_Ecommerce_Shop.Models;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coza_Ecommerce_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about/{slug?}")]
        public async Task <IActionResult> About (string? slug)
        {
            var listPost = await _postRepository.GetAllAsync();
            if (string.IsNullOrEmpty(slug))
            {
                // Nếu không có slug, trả về danh sách bài viết
                if (listPost == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy bài viết
                }
                listPost = listPost.OrderByDescending(x => x.CreateDate);
                var postmodel = listPost.First();   
                return View(postmodel);
            }

            // Nếu có slug, chỉ lấy bài viết đó
            var post = listPost.FirstOrDefault(x => x.Slug.ToLower() == slug.ToLower());

            if (post == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy bài viết
            }

            return View(post);
        }




		[Route("quick-view-product/{productId:guid}")]
		public async Task<IActionResult> LoadQuickProductView (Guid productId)
        {
			return await Task.FromResult(ViewComponent("QuickViewProduct", new { id = productId }));
		}


		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
