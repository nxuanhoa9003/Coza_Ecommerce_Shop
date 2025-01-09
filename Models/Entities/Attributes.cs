using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class Attributes : CommonAttributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Tên thuộc tính")]
        public string? AttributeName { get; set; }
       
        public virtual ICollection<AttributeValue>? AttributeValues { get; set; }
    }
}
