using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class ProductVariantAttribute : CommonAttributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductVariantId { get; set; }
        public int? AttributeId { get; set; }

        public string? Value { get; set; }
        public virtual ProductVariant? ProductVariant { get; set; }
        public virtual Attributes? Attributes { get; set; }

    }
}
