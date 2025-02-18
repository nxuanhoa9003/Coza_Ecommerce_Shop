using Microsoft.AspNetCore.Mvc;
using System;

namespace Coza_Ecommerce_Shop.Models.Helper
{
    public class UrlHandle
    {
        public UrlHandle()
        {
            ControllerName = string.Empty;
            Action = string.Empty;
        }

        public UrlHandle(string controllerName, string action)
        {
            ControllerName = controllerName;
            Action = action;
        }

        public string ControllerName { get; set; }
        public string Action { get; set; }
    }
}
