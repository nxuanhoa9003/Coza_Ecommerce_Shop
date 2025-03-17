using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Models.Helper;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using System.Linq;
using X.PagedList.Extensions;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/order")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMailService _mailService;
        private readonly EmailTemplateService _templateService;

        public OrderController(IOrderRepository orderRepository, IMailService mailService, EmailTemplateService templateService)
        {
            _orderRepository = orderRepository;
            _mailService = mailService;
            _templateService = templateService;
        }

        [HttpGet("")]
        [HttpGet("[action]")]
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var query = await _orderRepository.GetAllOrder();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Code.Contains(search));
            }

            var totalData = query.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;

            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            var pagedList = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);
            OrderPagingViewModel orderPagingView = new OrderPagingViewModel
            {
                Orders = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };
            return View(orderPagingView);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Details(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var order = await _orderRepository.GetOrderById(Id);


            if (order == null) return NotFound();


            var validStatuses = OrderStatusHelper.GetValidNextStatuses(order.Status, order.TypePayment)
            .Select(status => new SelectListItem
            {
                Value = status.ToString(),
                Text = OrderStatusHelper.OrderStatusDisplayNames[status]
            })
            .ToList();

            ViewBag.ValidStatuses = validStatuses;

           
            return View(order);
        }

        [HttpPost("update-status-order")]
        [Authorize(Policy = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, string newStatus)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null) return NotFound("Không tìm thấy đơn hàng.");


            // Chuyển đổi order.Status (string) thành OrderStatus (enum)
            if (!OrderStatusHelper.OrderStatusMap.TryGetValue(order.Status, out var currentStatusEnum))
            {
                return BadRequest("Trạng thái đơn hàng không hợp lệ.");
            }

            if (currentStatusEnum == OrderStatus.Completed)
            {
                return BadRequest("Không thể cập nhật đơn hàng đã hoàn thành.");
            }
            if (currentStatusEnum == OrderStatus.Canceled)
            {
                return BadRequest("Không thể cập nhật đơn hàng đã huỷ.");
            }

            // Chuyển đổi newStatus (string) thành OrderStatus (enum)
            if (!OrderStatusHelper.OrderStatusMap.TryGetValue(newStatus, out var newStatusEnum))
            {
                return BadRequest("Trạng thái mới không hợp lệ.");
            }

            // Kiểm tra trạng thái chuyển đổi có hợp lệ không
            if (!OrderStatusHelper.ValidTransitions.ContainsKey(currentStatusEnum) ||
                !OrderStatusHelper.ValidTransitions[currentStatusEnum].Contains(newStatusEnum))
            {
                return BadRequest("Trạng thái chuyển đổi không hợp lệ.");
            }

            // Nếu là COD, không cần qua trạng thái WaitingForPayment
            if (PaymentType.COD == order.TypePayment.ToUpper() && newStatusEnum == OrderStatus.WaitingForPayment)
            {
                return BadRequest("Đơn hàng COD không cần chờ thanh toán.");
            }

            // Nếu là VNPAY, không thể chuyển từ Pending -> Confirmed trực tiếp
            if (PaymentType.VNPAY == order.TypePayment.ToUpper() &&
                currentStatusEnum == OrderStatus.Pending &&
                newStatusEnum == OrderStatus.Confirmed)
            {
                return BadRequest("Đơn hàng VNPAY cần thanh toán trước khi xác nhận.");
            }


            if(order.Status.ToUpper() != newStatus.ToUpper())
            {
                // Cập nhật trạng thái hợp lệ
                order.Status = newStatus;
                var rs = await _orderRepository.UpdateStatusOrder(order);
                if(rs)
                {
                    await SendMailOrder(order, newStatus);
                }
                return Ok("Cập nhật trạng thái thành công.");
            }
            return BadRequest("Cập nhật trạng thái thất bại.");

        }

        private string GetImageUrl(string baseUrl, string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return $"{baseUrl}/images/default-product.jpg"; // Ảnh mặc định nếu không có ảnh

            return imagePath.StartsWith("/") ? $"{baseUrl}{imagePath}" : $"{baseUrl}/{imagePath}";
        }
        private async Task SendMailOrder(Order order, string newstatus)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            string orderItemsHtml = "";
            foreach (var item in order.OrderDetails)
            {
                string imageUrl = GetImageUrl(baseUrl, item.Product.Image);
                orderItemsHtml += $"<tr>" +
                                  $"<td><img src='{imageUrl}' class='product-image'></td>" +
                                  $"<td>{item.Product.Title}<br><small>Color: {item.Variant.Color} | Size: {item.Variant.Size}</small></td>" +
                                  $"<td>{item.Quantity}</td>" +
                                  $"<td>{item.Price.ToString("#,0")} VNĐ</td>" +
                                  $"<td>{(item.Quantity * item.Price).ToString("#,0")} VNĐ</td>" +
                                  $"</tr>";
            }

            var placeholders = new Dictionary<string, string>
            {
                { "CustomerName", order.FullName },
                { "OrderCode", order.Code },
                { "OrderDate", order.CreatedAt.ToString("HH:mm:ss dd/MM/yyyy") },
                { "OrderStatus", newstatus },
                { "TotalAmount", order.TotalAmount.ToString("#,0") + " VNĐ" },
                { "CustomerEmail", order.Email },
                { "CustomerPhone", order.Phone },
                { "CustomerAddress", order.Address },
                { "OrderItems", orderItemsHtml },
            };

            var emailBody = await _templateService.GetEmailTemplateAsync("confirm_order", placeholders);
            await _mailService.SendEmailAsync(order.Email, "Xác nhận đơn hàng", emailBody);
        }

    }
}
