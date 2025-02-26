using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
	public interface ITransactionRepository
	{
		Task CreateTransaction(Transaction model);
	}
}
