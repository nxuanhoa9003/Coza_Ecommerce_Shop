using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class AttributeValue : CommonAttributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AttributeId { get; set; }

        [StringLength(200)]
        [Display(Name ="Giá trị")]
        public string? Value { get; set; }

        public bool IsDeleted { get; set; } = false;
        // Navigation property
        public virtual Attributes? Attribute { get; set; }
    }
}
