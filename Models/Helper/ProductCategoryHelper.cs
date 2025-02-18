using Coza_Ecommerce_Shop.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coza_Ecommerce_Shop.Models.Helper
{
    public static class ProductCategoryHelper
    {
        public static List<SelectListItem> BuildCategorySelectList(List<ProductCategory> categories, Guid? parentId = null, int level = 0)
        {
            var list = new List<SelectListItem>();

            var filteredCategories = categories.Where(c => c.ParentCategoryId == parentId).ToList();
            foreach (var category in filteredCategories)
            {

                string indent = new string('\u00A0', level * 4);

                list.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = indent + category.Title

                });

                list.AddRange(BuildCategorySelectList(categories, category.Id, level + 1));
            }

            return list;
        }
    }
}
