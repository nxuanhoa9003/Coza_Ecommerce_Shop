using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Coza_Ecommerce_Shop.Extentions;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Post")]
    public class Post : CommonAttributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(150)]
        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }
        [Display(Name = "Slug URL")]
        public string? Slug { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }
        public string? Image { get; set; }
        
        [Display(Name = "Loại danh mục")]
        [Required(ErrorMessage = "Danh mục không được để trống")]
        [ValidateGuid(ErrorMessage = "Vui lòng chọn danh mục hợp lệ.")]
        public Guid? CategoryId { get; set; }

        public string? SeoTitile { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public Category? Category { get; set; }
    }
}
