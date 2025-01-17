using Entities = Coza_Ecommerce_Shop.Models.Entities;
using ViewModel = Coza_Ecommerce_Shop.ViewModels;
namespace Coza_Ecommerce_Shop.ViewModels.Home
{
    public class QuickViewProductVM
    {
        public Entities.Product productInfo { get; set; }
        public IEnumerable<Entities.ProductImage> images { get; set; } = new List<Entities.ProductImage>();
        public IEnumerable<ViewModel.Product.ProductVariantsViewModel> varaints { get; set; } = new List<ViewModel.Product.ProductVariantsViewModel>();
      
    }
}
