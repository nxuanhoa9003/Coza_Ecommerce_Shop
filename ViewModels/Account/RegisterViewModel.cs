using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Phải nhập {0}")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu", Prompt = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Lặp lại mật khẩu", Prompt = "Lặp lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu lặp lại không chính xác.")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Tên đăng nhập", Prompt = "Tên đăng nhập")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
        public string UserName { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "Họ và tên", Prompt = "Họ và tên")]
		[Required(ErrorMessage = "Phải nhập {0}")]
		[StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
		public string FullName { get; set; }
	}
}
