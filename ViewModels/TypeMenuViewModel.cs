using Entities= Coza_Ecommerce_Shop.Models.Entities;
namespace Coza_Ecommerce_Shop.ViewModels
{
	public class TypeMenuViewModel
	{
		public bool IsDesktop { get; set; }
		public List<Entities.ProductCategory> Categories { get; set; } = new List<Entities.ProductCategory>();
	}
}
