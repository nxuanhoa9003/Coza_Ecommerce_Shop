using Coza_Ecommerce_Shop.ViewModels.Cart;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Order
{
    public class CheckoutCartvViewModel
    {
        [MinLength(1, ErrorMessage = "Giỏ hàng không được để trống.")]
        public List<string> CartItem { get; set; } = new List<string>();

        public string? CodeCoupon { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh/thành.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn quận/huyện.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phường/xã.")]
        public string Ward { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ chi tiết.")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự.")]
        public string DetailAddress { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^(0[2-9][0-9]{8}|84[2-9][0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán.")]
        public string TypePayment { get; set; }

        [Required]
        public string FullAddress { set; get; }
        public ViewCartViewModel? viewCheckOutView { get; set; }
    }

}
