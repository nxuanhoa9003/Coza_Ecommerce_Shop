namespace Coza_Ecommerce_Shop.ViewModels.Product
{
    public class ParamsFilter
    {
        public int? page { set; get; }
        public string? slug { set; get; }
        public string? search { set; get; }
        public string? sort { set; get; }
        public int? minPrice { set; get; }
        public int? maxPrice { set; get; }
    }
}
