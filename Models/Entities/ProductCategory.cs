using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory : CommonAttributes
    {
        public ProductCategory() { 
            this.Products = new HashSet<Product>();
            this.CategoryChildren = new List<ProductCategory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string? Title { get; set; }

        [StringLength(150)]
        public string? Slug { get; set; }
        public string? Description { get; set; }

        [StringLength(150)]
        public string? SeoTitle { get; set; }
        [StringLength(500)]
        public string? SeoDescription { get; set; }
        [StringLength(250)]
        public string? SeoKeywords { get; set; }
        public bool IsDeleted { get; set; } = false;
        [DisplayName("Featured")]
        public bool IsFeatured { get; set; } = false;

        public ICollection<Product> Products { get; set; }


        // Khoá ngoại đến bảng cha nếu có
        [ForeignKey("ParentCategory")]
        [DisplayName("Parent Category")]
        public int? ParentCategoryId { get; set; }

        // Danh mục cha (Navigation property)
        public ProductCategory? ParentCategory { get; set; }

        // Danh sách các danh mục con
        public ICollection<ProductCategory>? CategoryChildren { get; set; }


    }
}
