using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class ProductVariant : CommonAttributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? SKU { get; set; }
        public int ProductId { get; set; }
        public decimal? AdditionalPrice { get; set; }
        public int? Quantity { get; set; } = 0;
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = false;
        // Navigation property
        public virtual Product? Product { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? AttributesJson { get; set; }
       

    }
}
