﻿using Coza_Ecommerce_Shop.DTO;
using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory?> GetByIdAsync(Guid? id);
        Task<ProductCategory?> GetByIdAsNoTrackingAsync(Guid? id);
        Task<ProductCategory?> GetBySlugAsNoTrackingAsync(string slug);
		Task<IEnumerable<ProductCategory>> GetByFilterSlugAsNoTrackingAsync(string slug);
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<IEnumerable<ProductCategory>> GetAllExceptIdAsync(Guid? id);

        Task<bool> AddAsync(ProductCategory productCategory);

        Task<bool> RemoveAsync(ProductCategory productCategory);
        Task<bool> UpdateAsync(ProductCategory productCategory);

        Task<bool> IsCategoryExistsAsync(ProductCategory pnew, ProductCategory? pold = null);

    }
}
