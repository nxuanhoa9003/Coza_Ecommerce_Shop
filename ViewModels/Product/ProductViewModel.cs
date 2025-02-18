using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductViewModel
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

        [Display(Name = "Người tạo")]
        public string? CreateBy { set; get; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateDate { set; get; } = DateTime.Now;
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifierDate { set; get; } = DateTime.Now;
        [Display(Name = "Cập nhập bởi")]
        public string? ModifiedBy { set; get; }

        // other
        public string? DeletedImageIds { set; get; } = string.Empty;

        public string? ErrorVariant { set; get; } = string.Empty;

        public Guid DefaultVariantId { get; set; } = Guid.Empty;

        [NotMapped]
        [Required(ErrorMessage = "Chọn ít nhất một ảnh.")]
        [DataType(DataType.Upload)]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public ICollection<ProductVariantsViewModel> Variants { get; set; } = new HashSet<ProductVariantsViewModel>();
        public ICollection<ProductImageViewModel>? ProductImages { get; set; } = new List<ProductImageViewModel>();

    }
}
