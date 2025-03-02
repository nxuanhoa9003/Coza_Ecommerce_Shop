using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class BannerRepository : IBannerRepository
    {
        private readonly AppDbContext _context;

        public BannerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(Banner banner)
        {
            await _context.Banners.AddAsync(banner);
            var rs = await _context.SaveChangesAsync();
            return rs > 0 ? (true, "Thêm thành công banner") : (false, "Tạo banner thất bại");
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(Banner banner)
        {
            var checkBanner = await _context.Banners.FindAsync(banner.Id);
            if (checkBanner == null)
            {
                return (false, "Không tìm thấy banner");
            }
            _context.Banners.Remove(checkBanner);
            var rs = await _context.SaveChangesAsync();
            return rs > 0 ? (true, "Xóa banner thành công") : (false, "Xóa banner thất bại");
        }

        public async Task<IEnumerable<Banner>> GetBannersAsync()
        {
            return await _context.Banners.ToListAsync();
        }

        public async Task<IEnumerable<Banner>> GetBannersShowAsync()
        {
            return await _context.Banners.Where(x => x.IsShow).ToListAsync();
        }

        public async Task<Banner> GetByIdAsync(string Id)
        {
            return await _context.Banners.FirstOrDefaultAsync(x => x.Id == Guid.Parse(Id));
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(Banner banner)
        {
            var checkBanner = await _context.Banners.FindAsync(banner.Id);
            if (checkBanner == null)
            {
                return (false, "Không tìm thấy banner");
            }

            _context.Update(banner);
            var rs = await _context.SaveChangesAsync();
            return rs > 0 ? (true, "Cập nhật banner thành công") : (false, "Cập nhật banner thất bại");

        }
    }
}
