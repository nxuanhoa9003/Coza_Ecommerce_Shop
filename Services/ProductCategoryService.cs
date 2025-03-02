using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Home;
using Coza_Ecommerce_Shop.ViewModels.Product;
using System.Linq;

namespace Coza_Ecommerce_Shop.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductCategoryService(
            IProductCategoryRepository productCategoryRepository,
            IProductRepository productRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }

        // Lấy danh sách danh mục sản phẩm và ánh xạ sản phẩm
        public async Task<ProductOverViewVM> GetProductCategoriesAsync(int page, int pageSize, ParamsFilter? filter = null)
        {
            var allCategories = (await _productCategoryRepository.GetAllAsync()).ToList();
            var parentCategories = new List<ProductCategory>();
            var categoryShow = new List<ProductCategory>();
            if (!string.IsNullOrEmpty(filter?.slug))
            {
                parentCategories = allCategories.Where(c => c.Slug == filter.slug).ToList();
                categoryShow = allCategories.Where(c => parentCategories.Any(pc => pc.Id == c.ParentCategoryId)).ToList();
            }
            else
            {
                parentCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
                categoryShow = parentCategories;
            }

            var allCategoryIds = new HashSet<Guid>();
            foreach (var category in parentCategories)
            {
                allCategoryIds.Add(category.Id);
                GetAllChildCategoryIds(category.Id, allCategories, allCategoryIds);
            }
            var listproduct = await _productRepository.GetProductByIdProductCategoryAsync(allCategoryIds);
            
            if (filter != null)
            {
                if (filter.search != null)
                {
                    listproduct = listproduct.Where(x => x.Title.ToLower().Contains(filter.search.Trim().ToLower())).ToList();
                }


                if (filter.minPrice != null || filter.maxPrice != null)
                {
                    listproduct = listproduct
                                .Where(x => x.Variants.Any(v =>
                                    (filter.minPrice == null || v.BasePrice >= filter.minPrice) &&
                                    (filter.maxPrice == null || v.BasePrice <= filter.maxPrice)
                                ))
                                .ToList();
                }


                if (filter.sort != null)
                {
                    listproduct = filter.sort switch
                    {
                        "newest" => listproduct.OrderByDescending(x => x.CreateDate).ToList(),
                        "price-asc" => listproduct.OrderBy(x => x.Variants.FirstOrDefault(v => v.IsDefault)?.BasePrice).ToList(),
                        "price-desc" => listproduct.OrderByDescending(x => x.Variants.FirstOrDefault(v => v.IsDefault)?.BasePrice).ToList(),
                        _ => listproduct
                    };

                }
            }


            int totalProducts = listproduct.ToList().Count;
            int totalPages = totalProducts > 0 ? (int)Math.Ceiling((double)totalProducts / pageSize) : 1;

            // Điều chỉnh giá trị của `page` để tránh lỗi danh sách rỗng
            page = page > totalPages ? totalPages : page;
            page = page < 1 ? 1 : page;

            var rslistproduct = listproduct.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new ProductOverViewVM
            {
                page = page,
                totalProducts = listproduct.ToList().Count,
                categories = categoryShow,
                products = rslistproduct.ToList(),
            };
        }

        private void GetAllChildCategoryIds(Guid parentId, List<ProductCategory> allCategories, HashSet<Guid> categoryIds)
        {
            var childCategories = allCategories.Where(c => c.ParentCategoryId == parentId).ToList();

            foreach (var child in childCategories)
            {
                if (!categoryIds.Contains(child.Id))
                {
                    categoryIds.Add(child.Id);
                    GetAllChildCategoryIds(child.Id, allCategories, categoryIds);
                }
            }
        }



    }
}
