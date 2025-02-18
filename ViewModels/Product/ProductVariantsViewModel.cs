using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductVariantsViewModel
    {
        public Guid? Id { get; set; }
        public string? SKU { get; set; }
        public Guid ProductId { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal? BasePrice { get; set; } = 0;
        public decimal? PriceSale { get; set; } = 0;
        public int? Quantity { get; set; } = 0;
        [Display(Name = "Hiển thị")]
        public bool IsActive { get; set; } = false;
        [Display(Name = "Mặc định")]
        public bool IsDefault { get; set; } = false;
       
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
