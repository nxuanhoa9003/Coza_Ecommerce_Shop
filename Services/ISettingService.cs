using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Repositories.Interfaces;

namespace Coza_Ecommerce_Shop.Services
{
    public interface ISettingService
    {
        Task<string> GetSettingValueAsync(string key);
        Task<Dictionary<string, string>> GetAllSettingsAsync();
    }

    public class SettingService : ISettingService
    {
        private readonly ISettingConfigurationRepository _settingConfiguration;

        public SettingService(ISettingConfigurationRepository settingConfiguration)
        {
            _settingConfiguration = settingConfiguration;
        }

        public async Task<Dictionary<string, string>> GetAllSettingsAsync()
        {
            return await _settingConfiguration.GetAllSettingsAsync();
        }

        public async Task<string> GetSettingValueAsync(string key)
        {
          return await _settingConfiguration.GetSettingValueAsync(key);
        }

    }


}
