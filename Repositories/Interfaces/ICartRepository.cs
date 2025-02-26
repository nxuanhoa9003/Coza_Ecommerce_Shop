using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels.Cart;

namespace Coza_Ecommerce_Shop.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task<bool> AddToCartAsync(CartItemViewModel model);

        Task<(bool IsSuccess, string ErrorMessage)> UpdateCart(string productSku, string variantSku, int quantity);
        Task<(bool IsSuccess, string ErrorMessage)> RemoveFromCart(string productSku, string variantSku);

        Task<bool> ClearCartAsync(string userId);
        Task<decimal> GetTotalPriceAsync(string userId);

        Task<List<ViewCartItemViewModel>> GetCartItemsByUserIdAsync(string UserId);
        Task<List<ViewCartItemViewModel>> GetCartItemsByIdAsync(string UserId, List<string> cartitem);


        Task<(bool IsSuccess, string ErrorMessage)> CheckStotkItemInCart(string UserId, List<string> cartitems);
      
    }
}
