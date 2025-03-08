using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(Contact contact)
        {
            await _context.AddAsync(contact);
            var rs = await _context.SaveChangesAsync();
            return rs > 0 ? (true, "Gửi thành công") : (false, "Gửi thất bại");
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _context.Contacts.OrderByDescending(x => x.IsRead).ToListAsync();
        }

    }
}
