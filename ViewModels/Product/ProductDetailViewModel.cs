namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public ProductDetailInfoViewModel ProductInfo { get; set; }
        public List<ProductImageViewModel> Images { get; set; } = new List<ProductImageViewModel>();
        public List<ProductVariantsViewModel> Variants { get; set; } = new List<ProductVariantsViewModel>();
    }
}
