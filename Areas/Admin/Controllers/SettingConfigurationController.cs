using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Common;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class SettingConfigurationController : Controller
    {
        private readonly ISettingConfigurationRepository _settingConfigurationRepository;
        public INotyfService _notifyService { get; }

        public SettingConfigurationController(ISettingConfigurationRepository settingConfigurationRepository, INotyfService notifyService, AppDbContext context)
        {
            _settingConfigurationRepository = settingConfigurationRepository;
            _notifyService = notifyService;

        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var settings = await _settingConfigurationRepository.GetAllSettingsAsync();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Dictionary<string, string> settings, IFormFile Logo)
        {

            string? logoPath = null;

            if (Logo != null && Logo.Length > 0)
            {
                logoPath = await Utilities.UploadFileAsync(Logo, "Logo");
            }

            var (IsSuccess, ErrorMessage) = await _settingConfigurationRepository.UpdateSettingsAsync(settings, logoPath);

            if (IsSuccess)
            {
                _notifyService.Success("Cập nhật thành công");
            }
            else
            {
                _notifyService.Error(ErrorMessage);
            }

            return RedirectToAction("Update", "SettingConfiguration");

        }
    }
}
