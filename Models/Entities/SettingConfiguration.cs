using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("SettingConfiguration")]
    public class SettingConfiguration
    {
        [Key]
        [StringLength(50)]
        public string SettingKey { get; set; }
        [MaxLength]
        public string SettingValue { get; set; }
        [MaxLength]
        public string SettingDescription { get; set; }
    }
}
