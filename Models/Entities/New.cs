using Coza_Ecommerce_Shop.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("New")]
    public class New : CommonAttributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự.")]
        public string Title { get; set; }

        [Display(Name = "Slug URL")]
        [StringLength(250, ErrorMessage = "Không được vượt quá 250 ký tự.")]
        public string? Slug { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Chi tiết")]     
        public string Detail { get; set; }

        [StringLength(250)]
        public string? Image {  get; set; }

        [Display(Name = "Loại danh mục")]
        [Required(ErrorMessage ="Danh mục không được để trống")]
        [ValidateGuid(ErrorMessage = "Vui lòng chọn danh mục hợp lệ.")]
        public Guid? CategoryId { get; set; }
        [StringLength(250)]
        public string? SeoTitile { get; set; }
        [StringLength(500)]
        public string? SeoDescription { get; set; }
        [StringLength(250)]
        public string? SeoKeywords { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive {  get; set; }
        public Category? Category { get; set; }
            

    }
}
