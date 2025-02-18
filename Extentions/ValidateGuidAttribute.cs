using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Extentions
{
    public class ValidateGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
         
            if (value == null || (value is Guid guidValue && guidValue == Guid.Empty))
            {
                return new ValidationResult(ErrorMessage ?? "Vui lòng chọn danh mục hợp lệ.");
            }
            return ValidationResult.Success;
        }
    }
}
