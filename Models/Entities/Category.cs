using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coza_Ecommerce_Shop.Models.Entities
{
    [Table("Category")]
    public class Category : CommonAttributes
    {

        public Category() {
            this.News = new HashSet<New>();
            this.Posts = new HashSet<Post>();
            this.Products = new HashSet<Product>();
        }
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(200, ErrorMessage = "Không được vượt quá 200 ký tự.")]
        public string? Title { get; set; }
        [Display(Name = "Slug URL")]
        [StringLength(250, ErrorMessage = "Không được vượt quá 250 ký tự.")]
        public string? Slug { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
       
        [Display(Name = "SEO Title")]
        [StringLength(250, ErrorMessage = "SEO Title không thể vượt quá 250 ký tự.")]
        public string? SeoTitle { get; set; }
        [Display(Name = "SEO Description")]
        [StringLength(500, ErrorMessage = "SEO Description không thể vượt quá 500 ký tự.")]
        public string? SeoDescription { get; set; }
        [Display(Name = "SEO Keywords")]
        [StringLength(500, ErrorMessage = "SEO Keywords không thể vượt quá 500 ký tự.")]
        public string? SeoKeywords { get; set; }
        [Display(Name = "Thứ tự")]
        public int? Position { get; set; }
        
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Product> Products { get; set; }




    }
}
