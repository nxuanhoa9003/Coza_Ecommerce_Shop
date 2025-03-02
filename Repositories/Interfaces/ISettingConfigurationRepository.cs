using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
	public interface ISettingConfigurationRepository
	{
		Task<string> GetSettingValueAsync(string key);
		Task<SettingConfiguration?> GetSettingAsync(string key);
		Task<bool> UpdateSettingAsync(SettingConfiguration setting);
		Task<bool> UpdateSettingValueAsync(string key, string value);
		Task<Dictionary<string, string>> GetAllSettingsAsync();
		Task<(bool IsSuccess, string ErrorMessage)> UpdateSettingsAsync(Dictionary<string, string> settings, string? logoPath = null);

    }
}
