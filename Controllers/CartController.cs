using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserService _userService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartRepository cartRepository, UserService userService, ILogger<CartController> logger)
        {
            _cartRepository = cartRepository;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");
            if (userPrincipal == null)
            {
                _logger.LogError("Lỗi: Người dùng chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }
            var userId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("Lỗi: Không tìm thấy claim Id của người dùng.");
                return BadRequest(new { message = "Không thể xác định tài khoản" });
            }

            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            ViewCartViewModel model = new ViewCartViewModel
            {
                couponCode = string.Empty,
                cartitems = cartItems,
                totalPrice = cartItems.Sum(x => x.total),
            };

            return View(model);
        }



        // ajax call


        [HttpGet("cart-items")]
        public async Task<IActionResult> GetCartItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Bạn chưa đăng nhập" });
            }

            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            return Ok(new { data = cartItems });
        }

        [HttpGet("count-cart-items")]

        public async Task<IActionResult> GetCountCartItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Bạn chưa đăng nhập" });
            }

            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            var countNumber = cartItems?.Count ?? 0;
            return Ok(new { count = countNumber });
        }


        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemFormViewModel model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");

            if (userPrincipal == null)
            {
                _logger.LogError("Lỗi: Không lấy được thông tin người dùng.");
                return Unauthorized(new { message = "Bạn chưa đăng nhập" });
            }


            var userId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("Lỗi: Không tìm thấy claim Id của người dùng.");
                return BadRequest(new { message = "Không thể xác định tài khoản" });
            }

            var CartItemViewModel = new CartItemViewModel
            {
                UserId = userId,
                ProductSku = model.productSku,
                VariantSku = model.variantSku,
                Quantity = model.quantity
            };

            var result = await _cartRepository.AddToCartAsync(CartItemViewModel);
            if (result)
            {
                return Ok(new { susscess = true, message = "Thêm thành công" });
            }

            _logger.LogError($"Lỗi: Thêm sản phẩm thất bại (UserId: {userId}, ProductSku: {model.productSku}, VariantSku: {model.variantSku}, Quantity: {model.quantity})");
            return BadRequest(new { message = "Thêm thất bại" });
        }



        [HttpPost("update-cart")]
        public async Task<IActionResult> UpdateCart(string productSku, string variantSku, int quantity)
        {
            var (IsSuccess, ErrorMessage) = await _cartRepository.UpdateCart(productSku, variantSku, quantity);
            if (IsSuccess)
            {
                return Ok(new { success = true, message = ErrorMessage });
            }
            return BadRequest(new { message = ErrorMessage });

        }

        [HttpPost("remove-from-cart")]
        public async Task<IActionResult> RemoveFromCart(string productSku, string variantSku)
        {
            var (IsSuccess, ErrorMessage) = await _cartRepository.RemoveFromCart(productSku, variantSku);
            if (IsSuccess)
            {
                return Ok(new { success = true, message = ErrorMessage });
            }
            return BadRequest(new { message = ErrorMessage });
        }


    }
}
