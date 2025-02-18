using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coza_Ecommerce_Shop.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [StringLength(400)]
        public string? AvatarUrl { get; set; }

        // Thời gian reset pass cuối cùng
        public DateTime? LastPasswordResetRequest { get; set; }
        // số lần nhập sai token khi reset pass
        public int FailedPasswordResetAttempts { get; set; } = 0;
        // Thời gian khoá reset pass cuối cùng
        public DateTime? PasswordResetLockoutEnd { get; set; }

    }
}
