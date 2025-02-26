using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string UserId {  get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal TotalPrice {  get; set; }
        public AppUser User { get; set; }

        [InverseProperty("Cart")]
        public List<CartItem> CartItems { get; set; } = new();
    }

    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid CartId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid VariantId { get; set; }
       
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("VariantId")]
        public ProductVariant Variant { get; set; }

    }

}
