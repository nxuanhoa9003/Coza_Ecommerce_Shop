using Coza_Ecommerce_Shop.Models.Entities;
using System.Reflection;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IBannerRepository
    {
        Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(Banner banner);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(Banner banner);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(Banner banner);
        Task<Banner> GetByIdAsync(string Id);
        Task<IEnumerable<Banner>> GetBannersAsync();
        Task<IEnumerable<Banner>> GetBannersShowAsync();
    }
}
