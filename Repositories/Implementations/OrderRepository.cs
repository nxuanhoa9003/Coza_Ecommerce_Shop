using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IVnPayService _vnPayService;

        public OrderRepository(AppDbContext context, IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }

        public async Task<(bool IsSuccess, string RedirectUrl, string ErrorMessage)> CreateOrder(Order order, string TypePayment = "COD")
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //Thêm đơn hàng vào database
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    if (TypePayment.ToUpper() == "COD")
                    {

                        foreach (var item in order.OrderDetails)
                        {
                            var product = await _context.ProductVariants.FindAsync(item.VariantId);
                            if (product == null)
                            {
                                throw new Exception($"Không tìm thấy sản phẩm với ID {item.ProductId}");
                            }

                            // Kiểm tra tồn kho
                            if (product.Quantity - product.ReservedStock < item.Quantity)
                            {
                                throw new Exception($"Sản phẩm {product.Product.Title} ({"color: " + product.Color + ", size: " + product.Size} không đủ hàng tồn kho!");
                            }
                            product.ReservedStock += item.Quantity; // Giữ hàng tạm thời
                            await _context.SaveChangesAsync();
                        }
                    }
                    else if (TypePayment.ToUpper() == "VNPAY")
                    {
                        // Gọi API VNPay để tạo thanh toán
                        var paymentUrl = await _vnPayService.CreatePaymentUrl(order);

                        if (string.IsNullOrEmpty(paymentUrl))
                        {
                            throw new Exception("Không thể tạo URL thanh toán VNPay.");
                        }

                        // Commit trước khi redirect sang VNPay
                        await transaction.CommitAsync();
                        return (true, paymentUrl, string.Empty);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return (true, string.Empty, string.Empty);
                }
                catch (Exception ex)
                {
                    // Rollback nếu có lỗi
                    await transaction.RollbackAsync();
                    return (false, string.Empty, ex.Message);
                }
            }
        }


        public async Task<Order?> GetOrderByTransactionId(string transactionId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Transaction.TransactionId == transactionId);
        }

        public async Task<Order?> GetOrderByOrderIdUserId(string orderId, string userId)
        {
            return await _context.Orders.Include(x => x.OrderDetails)
                            .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Variants)
                            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(orderId) && x.UserId == userId);
        }

        public async Task UpdateStatusOrder(string transactionId, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Transaction.TransactionId == transactionId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateProductQuantityAsync(Order order)
        {
            var orderModel = await _context.Orders
                   .Include(o => o.OrderDetails)
                   .ThenInclude(d => d.Product) // Load Product trong OrderDetails
                   .ThenInclude(p => p.Variants) // Load Variants của Product
                   .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (orderModel == null || orderModel.OrderDetails == null)
            {
                return false;
            }

            foreach (var item in orderModel.OrderDetails)
            {
                var product = item.Product;
                if (product != null)
                {
                    if (product.Quantity >= item.Quantity) // Kiểm tra tồn kho
                    {
                        product.Quantity -= item.Quantity;
                    }
                    else
                    {
                        return false; // Trả về false nếu hết hàng
                    }

                    var variant = product.Variants?.FirstOrDefault(x => x.Id == item.VariantId);
                    if (variant != null)
                    {
                        if (variant.Quantity >= item.Quantity)
                        {
                            variant.Quantity -= item.Quantity;
                        }
                        else
                        {
                            return false; // Hết hàng biến thể
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string UserId)
        {
            return await _context.Orders.Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _context.Orders.OrderBy(x => x.CreatedAt).ToListAsync();
        }

        public async Task<Order?> GetOrderById(string orderId)
        {
            return await _context.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(v => v.Variants)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(orderId));
        }


        public async Task<bool> UpdateStatusOrder(Order order)
        {
            _context.Update(order);
            if (order.TypePayment == "COD" && order.Status.ToLower() == "Confirmed")
            {
               await  UpdateProductQuantityAsync(order);
            }

            var rs = await _context.SaveChangesAsync();
            return rs > 0;
        }
    }
}
