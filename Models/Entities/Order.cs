using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Coza_Ecommerce_Shop.Models.Helper;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
            this.Code = GenerateOrderCode(this.Id);
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount không hợp lệ")]

        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(20)")]
		public string Status { get; set; } = OrderStatus.Pending.ToString();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Cập nhập bởi")]
        public string? ModifiedBy { set; get; }
        [Required]
        public string FullName {  get; set; }
        [Required]
        public string Phone {  get; set; }
        [Required]
        public string Email {  get; set; }

        [Required]
        public string Address { set; get; }

        [Required]
        public string TypePayment { set; get; }

        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

		public virtual Transaction Transaction { get; set; } // Mối quan hệ 1 - 1
		private static string GenerateOrderCode(Guid id)
        {
            string idString = id.ToString("N"); 
            string last8Chars = idString.Substring(idString.Length - 8);
            return $"DH{last8Chars.ToUpper()}";
        }
    }
}
