using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

    }
}
