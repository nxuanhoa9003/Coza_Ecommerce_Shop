using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}
