namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class VerifyAccountViewModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsResend { get; set; } = false;
    }
}
