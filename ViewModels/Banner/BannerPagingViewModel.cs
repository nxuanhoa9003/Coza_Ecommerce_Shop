using X.PagedList;
using Entities = Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.ViewModels.Banner
{
    public class BannerPagingViewModel
    {
        public IPagedList<Entities.Banner>? Banners { get; set; }
        public PagingViewModel? PagingInfo { get; set; }
    }
}
