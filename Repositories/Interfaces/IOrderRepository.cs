using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<(bool IsSuccess, string RedirectUrl, string ErrorMessage)> CreateOrder(Order order, string TypePayment = "COD");
        Task<Order?> GetOrderByTransactionId(string transactionId);
        Task<Order?> GetOrderByOrderIdUserId(string orderId, string userId);
        Task<Order?> GetOrderById(string orderId);

        Task UpdateStatusOrder(string transactionId, string status);

        Task<bool> UpdateProductQuantityAsync(Order order);

        Task<IEnumerable<Order>> GetOrdersByUserId(string UserId);
        Task<IEnumerable<Order>> GetAllOrder();

        Task<bool> UpdateStatusOrder(Order order);

    }
}
