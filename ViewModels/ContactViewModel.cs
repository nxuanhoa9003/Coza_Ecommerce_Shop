using Coza_Ecommerce_Shop.Models.Entities;
using X.PagedList;

namespace Coza_Ecommerce_Shop.ViewModels
{
    public class ContactViewModel
    {
        public string email { set; get; }
        public string fullname { set; get; }
        public string message { set; get; }

    }

    public class ContactPagingViewModel
    {
        public IPagedList<Contact>? contacts { get; set; }
        public PagingViewModel PagingInfo { get; set; }
    }
}
