using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;

namespace Coza_Ecommerce_Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> GetProductByPage(int page, int pageSize)
        {
            var listProduct = await _productRepository.GetProductsHot();
            var rs = listProduct.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return rs;
        }
    }
}
