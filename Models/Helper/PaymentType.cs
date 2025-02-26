namespace Coza_Ecommerce_Shop.Models.Helper
{
    public static class PaymentType
    {

        public static string COD = "COD";
        public static string VNPAY = "VNPAY";


        public static readonly HashSet<string> ValidPaymentMethods = new()
        {
            "COD", "VNPAY"
        };

        public static bool IsValid(string paymentMethod)
        {
            return ValidPaymentMethods.Contains(paymentMethod);
        }
    }
}
