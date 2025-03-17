using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(Contact contact);
        Task<IEnumerable<Contact>> GetContacts();

        Task<Contact?> GetContactById(string id);
    }
}
