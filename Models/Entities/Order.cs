using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Order")]
    public class Order : CommonAttributes
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }

        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
