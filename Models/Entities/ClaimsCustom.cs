using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    public class ClaimsCustom
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        [Display(Name = "Tên quyền")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Loại claim không được để trống")]
        [StringLength(50, ErrorMessage = "Loại claim không được vượt quá 50 ký tự")]
        [Display(Name = "Loại quyền")]
        public string ClaimType { get; set; }

        [Required(ErrorMessage = "Giá trị claim không được để trống")]
        [StringLength(255, ErrorMessage = "Giá trị claim không được vượt quá 255 ký tự")]
        [Display(Name = "Giá trị quyền")]
        public string ClaimValue { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Loại claim không được để trống")]
        [StringLength(50, ErrorMessage = "Loại claim không được vượt quá 50 ký tự")]
        [Display(Name = "Loại claim")]
        public string TypeClaim { get; set; }
    }
}
