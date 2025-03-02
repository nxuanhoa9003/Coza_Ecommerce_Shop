using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels
{
    public class PagingViewModel
	{
		public int CurrentPage { get; set; }
        public int TotalPages { get; set; } // Tổng số trang
        public int PageSize { get; set; } // Số bài viết hiển thị trên mỗi trang.
        public int TotalCount { get; set; } // Tổng số bài viết được lấy ra trong trang hiện tại

        // other
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
        public string? PriceRange { get; set; }
        public string? Slug { get; set; }
        public int? PriceMin { get; set; }
        public int? PriceMax { get; set; }
    }
}
