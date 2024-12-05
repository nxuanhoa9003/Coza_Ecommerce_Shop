using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Coza_Ecommerce_Shop.Models.Common
{
    public static class FilterChar
    {
        public static string GenerateSlug(string input)
        {
            // Bước 1: Chuyển thành chữ thường
            input = input.ToLowerInvariant();

            // Bước 2: Loại bỏ dấu tiếng Việt
            input = RemoveVietnameseDiacritics(input);

            // Bước 3: Loại bỏ ký tự không hợp lệ
            input = Regex.Replace(input, @"[^a-z0-9\s-]", "");

            // Bước 4: Thay khoảng trắng và dấu gạch ngang thừa thành 1 dấu gạch ngang duy nhất
            input = Regex.Replace(input, @"\s+", " ").Trim();
            input = Regex.Replace(input, @"\s", "-");

            return input;
        }
        private static string RemoveVietnameseDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
