using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class SettingConfigurationRepository : ISettingConfigurationRepository
    {
        private readonly AppDbContext _context;

        public SettingConfigurationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllSettingsAsync()
        {
            return await _context.SettingConfigurations
                        .ToDictionaryAsync(s => s.SettingKey, s => s.SettingValue);
        }

        public async Task<SettingConfiguration?> GetSettingAsync(string key)
        {
            return await _context.SettingConfigurations
                                .FirstOrDefaultAsync(s => s.SettingKey == key);

        }

        public async Task<string> GetSettingValueAsync(string key)
        {
            var setting = await _context.SettingConfigurations
                                .FirstOrDefaultAsync(s => s.SettingKey == key);
            return setting?.SettingValue ?? "";
        }

        public async Task<bool> UpdateSettingAsync(SettingConfiguration setting)
        {
            var settingOld = await _context.SettingConfigurations
                                .AnyAsync(s => s.SettingKey == setting.SettingKey);

            if (!settingOld) return false;
            _context.SettingConfigurations.Update(setting);
            var rs = await _context.SaveChangesAsync();
            return rs > 0 ? true : false;
        }

        public async Task<bool> UpdateSettingValueAsync(string key, string value)
        {
            var setting = await _context.SettingConfigurations.FirstOrDefaultAsync(s => s.SettingKey == key);
            if (setting != null)
            {
                setting.SettingValue = value;
                _context.SettingConfigurations.Update(setting);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateSettingsAsync(Dictionary<string, string> settings, string? logoPath = null)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var setting in settings)
                {
                    if (setting.Key == "__RequestVerificationToken" || setting.Key == "Logo")
                        continue;
                    var existingSetting = await _context.SettingConfigurations.FirstOrDefaultAsync(s => s.SettingKey == setting.Key);
                    if (existingSetting != null)
                    {
                        existingSetting.SettingValue = setting.Value;
                    }
                    else
                    {
                        _context.SettingConfigurations.Add(new SettingConfiguration
                        {
                            SettingKey = setting.Key,
                            SettingValue = setting.Value
                        });
                    }
                    
                }

                await _context.SaveChangesAsync();

                // Xử lý logo nếu có
                if (!string.IsNullOrEmpty(logoPath))
                {
                    var logoSetting = await _context.SettingConfigurations.FirstOrDefaultAsync(s => s.SettingKey == "Logo");
                    if (logoSetting != null)
                    {
                        var oldLogo = logoSetting.SettingValue;
                        logoSetting.SettingValue = logoPath;

                        // Nếu cập nhật thành công thì xóa ảnh cũ
                        var result = await _context.SaveChangesAsync();
                        if (result > 0)
                        {
                            Utilities.DeleteImage(oldLogo);
                        }
                        else
                        {
                            Utilities.DeleteImage(logoPath);
                            throw new Exception("Cập nhật logo thất bại.");
                        }
                    }
                    else
                    {
                        _context.SettingConfigurations.Add(new SettingConfiguration
                        {
                            SettingKey = "Logo",
                            SettingValue = logoPath
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, "Cập nhật thành công.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }
}
