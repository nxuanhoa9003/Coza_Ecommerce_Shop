using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Product")]
    public class Product : CommonAttributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [DisplayName("SKU")]
        public string ProductCode { get; set; }
        [Required]
        [StringLength(250)]
        [DisplayName("Tên sản phẩm")]
        public string Title { get; set; }

        public string? Slug { get; set; }
        public string? Description { get; set; }
        [DisplayName("Chi tiết")]
        public string? Detail { get; set; }

        [StringLength(250)]
        [DisplayName("Ảnh đại diện")]
        public string? Image { get; set; }
        [DisplayName("Giá")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [DisplayName("Giá khuyến mãi")]
        [Range(0, double.MaxValue)]
        public decimal? PriceSale { get; set; }
        [DisplayName("Số lượng tổng")]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public bool IsHome { get; set; } = true;
        [DisplayName("Khuyến mãi")]
        public bool IsSale { get; set; }
        [DisplayName("Nổi bật")]
        public bool IsFeature { get; set; }
        [DisplayName("Hot")]
        public bool IsHot { get; set; }
        [DisplayName("Danh mục sản phẩm")]
        public int ProductCategoryId { get; set; }
        [StringLength(250)]
        public string? SeoTitile { get; set; }
        [StringLength(500)]
        public string? SeoDescription { get; set; }
        [StringLength(250)]
        public string? SeoKeywords { get; set; }

        [DisplayName("Hiển thị")]
        public bool IsActive { get; set; }
        [DisplayName("Danh mục sản phẩm")]
        public virtual ProductCategory? ProductCategory { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Vui lòng chọn ít nhất một ảnh.")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public virtual ICollection<ProductVariant>? Variants { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
    }
}
