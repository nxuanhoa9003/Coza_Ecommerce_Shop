using Entities = Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.DTO;

namespace Coza_Ecommerce_Shop.ViewModels.Home
{
    public class ProductOverViewVM
    {
        public List <ProductCategoryDTO> productCategoryDTO { get; set; } = new List <ProductCategoryDTO> ();
        public List <Entities.Product> products { get; set; } = new List <Entities.Product> ();
    }
}
