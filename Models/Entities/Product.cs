using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Product")]
    public class Product : CommonAttributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        [DisplayName("SKU")]
        public string? ProductCode { get; set; }
        
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

        [DisplayName("Số lượng tổng")]
        [Required]
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
        public bool IsHome { get; set; } = true;
        [DisplayName("Khuyến mãi")]
        public bool IsSale { get; set; }
        [DisplayName("Nổi bật")]
        public bool IsFeature { get; set; }
        [DisplayName("Hot")]
        public bool IsHot { get; set; }
        [DisplayName("Danh mục sản phẩm")]
        [Required(ErrorMessage = "Chọn danh mục sản phẩm.")]
        public Guid? ProductCategoryId { get; set; }
        [StringLength(250)]
        public string? SeoTitile { get; set; }
        [StringLength(500)]
        public string? SeoDescription { get; set; }
        [StringLength(250)]
        public string? SeoKeywords { get; set; }

        [DisplayName("Hiển thị")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string AttributeOptionIds { get; set; } = "[]";

        [DisplayName("Danh mục sản phẩm")]
        public ProductCategory? ProductCategory { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Chọn ít nhất một ảnh.")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public ICollection<ProductVariant> Variants { get; set; } = new HashSet<ProductVariant>();
        public ICollection<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();
    }
}
