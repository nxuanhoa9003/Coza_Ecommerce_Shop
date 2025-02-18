using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid? id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task AddAsync(Post postmodel);

        Task RemoveAsync(Post postmodel);
        Task RemoveRangeAsync(IEnumerable<Post> listposts);
        Task UpdateAsync(Post postmodel);
    }
}
