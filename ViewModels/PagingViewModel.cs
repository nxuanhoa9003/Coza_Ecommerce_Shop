using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels
{
    public class PagingViewModel
	{
		public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        // other
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
        public string? PriceRange { get; set; }
        public string? Slug { get; set; }
        public int? PriceMin { get; set; }
        public int? PriceMax { get; set; }
    }
}
