namespace Coza_Ecommerce_Shop.Services
{
    public class EmailTemplateService
    {
        private readonly IWebHostEnvironment _env;

        public EmailTemplateService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> GetEmailTemplateAsync(string templateName, Dictionary<string, string> placeholders)
        {
            var filePath = Path.Combine(_env.WebRootPath, "templates", $"{templateName}.html");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Không tìm thấy file template: {filePath}");

            var templateContent = await File.ReadAllTextAsync(filePath);

            // Thay thế các placeholder trong template
            foreach (var placeholder in placeholders)
            {
                templateContent = templateContent.Replace($"{{{placeholder.Key}}}", placeholder.Value);
            }

            return templateContent;
        }

    }
}
