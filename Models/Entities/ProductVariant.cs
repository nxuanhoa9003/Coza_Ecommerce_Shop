using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class ProductVariant : CommonAttributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? SKU { get; set; }
        public Guid ProductId { get; set; }

        public string? Color { get; set; }
        public string? Size { get; set; }

        public decimal? BasePrice { get; set; } = 0;
        public decimal? PriceSale { get; set; } = 0;

        [Required]
        public int? Quantity { get; set; } = 0;

        public int ReservedStock { get; set; } = 0;

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Mặc định")]
        public bool IsDefault { get; set; } = false;

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
