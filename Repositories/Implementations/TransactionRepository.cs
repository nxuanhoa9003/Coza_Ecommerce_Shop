using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly AppDbContext _context;

		public TransactionRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task CreateTransaction(Transaction model)
		{
			await _context.Transactions.AddAsync(model);
			await _context.SaveChangesAsync();
		}
	}
}
