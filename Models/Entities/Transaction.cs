using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models.Entities
{
	public class Transaction
	{
		[Key]
		[MaxLength(30)]
		public string TransactionId { get; set; }// Lưu theo format "Ticks + Random"

		[Required]
		public Guid OrderId { get; set; } // Liên kết với Orders

		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; } // Khai báo quan hệ với Order

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
