using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class RegisterWithGoogleViewModel
    {
        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

     
        [Required]
        public string Email { get; set; }

        
        [Required]
        public string ProviderKey { get; set; }
    }

    public class ConfirmLinkGoogleViewModel
    {
        [Required]
        public string Email { get; set; }


        [Required]
        public string ProviderKey { get; set; }
    }

}
