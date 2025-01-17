using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models
{
    public abstract class CommonAttributes
    {
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
