using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using elFinder.NetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context) {
            _context = context;
        } 
        public async Task AddAsync(Post postmodel)
        {
            await _context.Posts.AddAsync(postmodel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(x => x.Category).ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int? id)
        {
            return await _context.Posts.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(Post postmodel)
        {
            _context.Posts.Remove(postmodel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Post> listposts)
        {
            _context.Posts.RemoveRange(listposts);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post postmodel)
        {
            _context.Posts.Update(postmodel);
            await _context.SaveChangesAsync();
        }
    }
}
