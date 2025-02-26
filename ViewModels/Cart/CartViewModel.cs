using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Cart
{
    public class CartItemViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ProductSku { get; set; }

        [Required]
        public string VariantSku { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }
    }
    public class CartItemFormViewModel
    {

        [Required]
        public string productSku { get; set; }

        [Required]
        public string variantSku { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int quantity { get; set; }
    }


    public class ViewCartViewModel
    {

        public string? couponCode { get; set; }
        public List<ViewCartItemViewModel> cartitems { get; set; } = new();
        public decimal totalPrice { get; set; } 

    }

    public class ViewCartItemViewModel
    {
        public string CartItemId { get; set; }
        public string productSlug { get; set; }
        public string productImage { get; set; }
        public Guid productId { get; set; }
        public Guid variantId { get; set; }
        public string productSku { get; set; }
        public string variantSku { get; set; }
        public string productName { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }

    }


}
