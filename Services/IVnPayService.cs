using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels.VNPay;

namespace Coza_Ecommerce_Shop.Services
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(Order order);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
