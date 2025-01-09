using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductVariantsViewModel
    {
        public int Id { get; set; }
        public string? SKU { get; set; }
        public int ProductId { get; set; }
        public decimal? AdditionalPrice { get; set; }
        public int? Quantity { get; set; } = 0;
        [Display(Name = "Hiển thị")]
        public bool IsActive { get; set; } = false;

        public string? AttributesJson { get; set; }

        public ICollection<AttributeData>? Attributes { get; set; } = new List<AttributeData>();

        [Display(Name = "Người tạo")]
        public string? CreateBy { set; get; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateDate { set; get; } = DateTime.Now;
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifierDate { set; get; } = DateTime.Now;
        [Display(Name = "Cập nhập bởi")]
        public string? ModifiedBy { set; get; }
    }
}
