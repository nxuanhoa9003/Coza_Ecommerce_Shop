using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Models.Helper;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels.Cart;
using Coza_Ecommerce_Shop.ViewModels.Order;
using Humanizer;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("order")]
    public class OrderController : Controller
    {

        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserService _userService;
        private readonly ILogger<OrderController> _logger;
        private readonly IVnPayService _vnPayService;

        public INotyfService _notifyService { get; }

        public OrderController(ICartRepository cartRepository, IOrderRepository orderRepository, UserService userService, ILogger<OrderController> logger, IVnPayService vnPayService, INotyfService notifyService)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userService = userService;
            _logger = logger;
            _vnPayService = vnPayService;
            _notifyService = notifyService;
        }

        [HttpGet("")]
        [HttpGet("list-orders")]
        public async Task<IActionResult> ListOrders()
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

            var listorder = await _orderRepository.GetOrdersByUserId(userId);
            return View(listorder);
        }


        [HttpPost("request-checkout")]
        public async Task<IActionResult> RequestCheckout(List<string> cartitem, string? codecoupon)
        {
            if (cartitem == null || cartitem.Count <= 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");
            if (userPrincipal == null)
            {
                _logger.LogError("Lỗi: Người dùng chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }
            var userId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;


            var cartitemcheckout = await _cartRepository.GetCartItemsByIdAsync(userId, cartitem);
            var totalprice = cartitemcheckout.Sum(x => x.quantity * x.price);
            if (!string.IsNullOrEmpty(codecoupon))
            {
                // tìm code coupon 
                totalprice -= 0;
            }

            CheckoutCartvViewModel model = new CheckoutCartvViewModel
            {
                viewCheckOutView = new ViewCartViewModel
                {
                    cartitems = cartitemcheckout,
                    couponCode = codecoupon,
                    totalPrice = totalprice,
                }
            };


            return View("Index", model);
        }

        [HttpGet("view-checkout")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("handel-check-out")]
        public async Task<IActionResult> Index(CheckoutCartvViewModel model)
        {

            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");
            if (userPrincipal == null)
            {
                _logger.LogError("Lỗi: Người dùng chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }
            var userId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;


            var cartitemcheckout = await _cartRepository.GetCartItemsByIdAsync(userId, model.CartItem);
            var totalprice = cartitemcheckout.Sum(x => x.quantity * x.price);
            var totalquantity = cartitemcheckout.Sum(x => x.quantity);

            model.viewCheckOutView = new ViewCartViewModel
            {
                cartitems = cartitemcheckout?.ToList(),
                totalPrice = totalprice
            };

            if (!PaymentType.ValidPaymentMethods.Contains(model.TypePayment.ToUpper()))
            {
                _notifyService.Error("Phương thức thanh toán không hợp lệ");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (IsSuccessCheck, ErrorMessageCheck) = await _cartRepository.CheckStotkItemInCart(userId, model.CartItem);
            if (!IsSuccessCheck)
            {
                _logger.LogError(ErrorMessageCheck);
                return View(model);
            }


            Order order = new Order
            {
                TotalAmount = totalprice,
                Quantity = totalquantity,
                Status = model.TypePayment.ToUpper() == "COD" ? OrderStatus.Pending.ToString() : OrderStatus.WaitingForPayment.ToString(),
                UserId = userId,
                Address = model.FullAddress,
                FullName = model.FullName,
                Phone = model.Phone,
                Email = model.Email,
                TypePayment = model.TypePayment
            };

            order.OrderDetails = cartitemcheckout.Select(item => new OrderDetail
            {
                OrderId = order.Id,
                ProductId = item.productId,
                VariantId = item.variantId,
                Price = item.price,
                Quantity = item.quantity
            }).ToList();


            var (OrderSuccess, RedirectUrl, OrderError) = await _orderRepository.CreateOrder(order, model.TypePayment);
            if (!OrderSuccess)
            {
                _logger.LogError(OrderError);
                ModelState.AddModelError("", "Đặt hàng thất bại: " + OrderError);
                return View(model);
            }

            // Nếu là VNPay, chuyển hướng đến trang thanh toán
            if (!string.IsNullOrEmpty(RedirectUrl))
            {
                return Redirect(RedirectUrl);
            }

            _notifyService.Success("Đặt hàng thành công");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("vnpay-return")]
        public async Task<IActionResult> VNPayReturn()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            var transactionId = response.OrderTransactionId;

            var order = await _orderRepository.GetOrderByTransactionId(transactionId);
            if (order == null)
            {
                return BadRequest("Không tìm thấy order");
            }

            if (!response.Success)
            {
                // cập nhật trạng thái đơn hàng
                await _orderRepository.UpdateStatusOrder(transactionId, OrderStatus.PaymentFailed.ToString()); 
				return RedirectToAction("PaymentFail");
			}
            else
            {
                // cập nhật trạng thái đơn hàng
                await _orderRepository.UpdateStatusOrder(transactionId, OrderStatus.Paid.ToString());
                await _orderRepository.UpdateProductQuantityAsync(order);
				return RedirectToAction("PaymentSuccess", new { transactionId  = transactionId });
			}
        }


		[HttpGet("PaymentFail")]
		public IActionResult PaymentFail()
        {
            return View();
        }
		[HttpGet("PaymentSuccess")]
		public async Task<IActionResult> PaymentSuccess(string transactionId)
		{
            var order = await _orderRepository.GetOrderByTransactionId(transactionId);
			return View(order);
		}


        [HttpGet("detail/{id?}")]
        public async Task<IActionResult> Detail(string id)
        {
            var userPrincipal = await _userService.GetUserBySchemeAsync("CustomerScheme");
            if (userPrincipal == null)
            {
                _logger.LogError("Lỗi: Người dùng chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }
            var userId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            var order = await _orderRepository.GetOrderByOrderIdUserId(id, userId);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
