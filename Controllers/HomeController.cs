using Coza_Ecommerce_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coza_Ecommerce_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

		[Route("quick-view-product/{productId}")]
		public async Task<IActionResult> LoadQuickProductView (int productId)
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
