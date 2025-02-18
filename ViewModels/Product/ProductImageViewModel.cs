
namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductImageViewModel
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }   
    }
}
