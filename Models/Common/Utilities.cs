using elFinder.NetCore;

namespace Coza_Ecommerce_Shop.Models.Common
{
    public static class Utilities
    {
        private static readonly string[] ValidFileExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
        // Phương thức để upload file vào thư mục uploads
        public static async Task<string> UploadFileAsync(IFormFile file, string uploadFolder = "Uploads")
        {
            string pathroot = "Uploads";
            string urlfolder = uploadFolder != null ? pathroot + "/" + uploadFolder : pathroot;
            try
            {
                if (file == null || file.Length == 0) return null;
                string path = Path.Combine(Directory.GetCurrentDirectory(), urlfolder);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!ValidFileExtensions.Contains(extension))
                {
                    return null;
                }
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var fileExtension = Path.GetExtension(file.FileName);
                var fname = FilterChar.GenerateSlug(Path.GetFileNameWithoutExtension(file.FileName));
                var newFileName = fname + "_" + timestamp + fileExtension;

                var filePath = Path.Combine(path, newFileName);

                if (File.Exists(filePath))
                {
                    return '/' + Path.Combine(urlfolder, newFileName).Replace("\\", "/");
                }


                // Lưu file vào thư mục uploads
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return '/' + Path.Combine(urlfolder, newFileName).Replace("\\", "/");

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static void DeleteImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return; 
            if (System.IO.File.Exists(imageUrl))
            {
                // Xóa tập tin
                System.IO.File.Delete(imageUrl);
            }
        }

    }
}
