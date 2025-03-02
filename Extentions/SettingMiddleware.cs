using Coza_Ecommerce_Shop.Services;

namespace Coza_Ecommerce_Shop.Extentions
{
    public class SettingMiddleware
    {
        private readonly RequestDelegate _next;

        public SettingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISettingService settingService)
        {
            var settings = await settingService.GetAllSettingsAsync();
            context.Items["Settings"] = settings;
            await _next(context);
        }
    }
}
