using Coza_Ecommerce_Shop.Models.Helper;

namespace Coza_Ecommerce_Shop.ViewModels.Order
{
    public class UpdateStatusRequest
    {
        public string OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
