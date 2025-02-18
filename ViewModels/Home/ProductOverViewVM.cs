using Entities = Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels.Home
{
    public class ProductOverViewVM
    {
        public int page { get; set; } = 1;
        public int totalProducts { get; set; }
        public  List <Entities.Product> products { get; set; } = new List <Entities.Product> ();
        
        public List<Entities.ProductCategory> categories { get; set; } = new List<Entities.ProductCategory>();
        // filter
        public PagingViewModel? pagingViewModel { get; set; }

    }
}
