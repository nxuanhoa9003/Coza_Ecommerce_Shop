using Coza_Ecommerce_Shop.Models.Entities;
using Entities = Coza_Ecommerce_Shop.Models.Entities;
namespace Coza_Ecommerce_Shop.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Entities.Product>> GetProductByPage(int page, int pageSize);
    }
}
