using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }  
        public Guid ProductId { get; set; }
        public Guid VariantId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;


        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("VariantId")]
        public ProductVariant Variant { get; set; }
    }
}
