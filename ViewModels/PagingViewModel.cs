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
    }
}
