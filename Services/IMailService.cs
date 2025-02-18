namespace Coza_Ecommerce_Shop.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
