using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class Wishlist
    {
        [Key]
        public Guid Id { set; get; } = Guid.NewGuid();

        public Guid ProductId { get; set; }

        public string UserId { get; set; }

        public DateTime CreateDate { set; get; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public Product product { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }   


    }
}
