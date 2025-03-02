using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Banner")]
    public class Banner : CommonAttributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(200, ErrorMessage = "Không được vượt quá 200 ký tự.")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string? Description { get; set; }
        [Display(Name = "Ảnh")]
        [StringLength(500)]
        public string? Image { get; set; }

        [Display(Name = "Url")]
        public string? Link { get; set; }
        [Display(Name = "Thứ tự")]
        public int Type { get; set; }

        [Display(Name = "Hiển thị")]
        public bool IsShow { get; set; } = false;

    }
}
