using Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels
{
    public class PostViewModel
    {
        public IPagedList<Post>? Posts { get; set; }
        public PagingViewModel PagingInfo { get; set; }
    }
}
