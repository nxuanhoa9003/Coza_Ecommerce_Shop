﻿namespace Coza_Ecommerce_Shop.ViewModels.Account
{
    public class CustomerDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; } = "/uploads/avt_empty.jpg";
    }
}
