using Coza_Ecommerce_Shop.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductDetailInfoViewModel
    {  
        public int Id { get; set; }
        [StringLength(50)]
        [DisplayName("SKU")]
        public string? ProductCode { get; set; }
        [Required]
        [StringLength(250)]
        [DisplayName("Tên sản phẩm")]
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        
        [DisplayName("Chi tiết")]
        public string? Detail { get; set; }

        [StringLength(250)]
        [DisplayName("Ảnh đại diện")]
        public string? Image { get; set; }
        [Required]
        [DisplayName("Giá")]
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [DisplayName("Giá khuyến mãi")]
        [Range(0, double.MaxValue)]
        public decimal? PriceSale { get; set; }

        [Required]
        [DisplayName("Số lượng tổng")]
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
        public int? ProductCategoryId { get; set; }
        [StringLength(250)]
        public string? SeoTitile { get; set; }
        [StringLength(500)]
        public string? SeoDescription { get; set; }
        
        [StringLength(250)]
        public string? SeoKeywords { get; set; }

        [DisplayName("Hiển thị")]
        public bool IsActive { get; set; }

        [DisplayName("Danh mục")]
        public string? CategoryName { get; set; }

        [Display(Name = "Người tạo")]
        public string? CreateBy { set; get; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateDate { set; get; } = DateTime.Now;
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifierDate { set; get; } = DateTime.Now;
        [Display(Name = "Cập nhập bởi")]
        public string? ModifiedBy { set; get; }
        
        //for edit

        [Required(ErrorMessage = "Chọn ít nhất một ảnh.")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        public string? DeletedImageIds { get; set;}
    }
}
