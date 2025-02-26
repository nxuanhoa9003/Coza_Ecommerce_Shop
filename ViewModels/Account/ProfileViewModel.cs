using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class ProfileViewModel
    {
        public string? Id { get; set; }
        public string ? AvatarUrl { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Họ và tên không được quá 100 ký tự.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự.")]
        public string? Address { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }

        public IFormFile? fileImage { set; get; }
    }


    public class ProfilePasswordViewModel
    {

        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Mật khẩu cũ không được để trống.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmNewPassword { get; set; }
    }

}
