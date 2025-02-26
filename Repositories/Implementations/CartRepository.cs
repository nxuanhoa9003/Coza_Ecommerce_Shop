using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;

namespace Coza_Ecommerce_Shop.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {

        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToCartAsync(CartItemViewModel model)
        {

            var product = await _context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.ProductCode == model.ProductSku);

            if (product == null) return false;

            var variant = product.Variants.FirstOrDefault(x => x.SKU == model.VariantSku);
            if (variant == null) return false;

            var cart = await GetCartByUserIdAsync(model.UserId);
            if (cart == null)
            {
                cart = new Cart { UserId = model.UserId, TotalPrice = 0 };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci =>
                    ci.ProductId == product.Id && ci.VariantId == variant.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += model.Quantity;
            }
            else
            {

                if (product == null || variant == null) return false; // Không tìm thấy sản phẩm hoặc biến thể
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    VariantId = variant.Id,
                    Quantity = model.Quantity,
                    Price = variant.BasePrice.Value
                };
                _context.CartItems.Add(newItem);
            }

            cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price * ci.Quantity);

            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            try
            {
                if (string.IsNullOrEmpty(UserId)) return false;
                var cart = await _context.Carts.Include(x => x.CartItems).FirstOrDefaultAsync(x => x.UserId == UserId);
                if (cart == null || !cart.CartItems.Any()) return false;

                _context.RemoveRange(cart.CartItems);
                cart.TotalPrice = 0;
                cart.CartItems.Clear();
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex) // Lỗi cập nhật database
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex) // Lỗi khác
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }

        }

        public async Task<Cart?> GetCartByUserIdAsync(string UserId)
        {
            if (string.IsNullOrEmpty(UserId)) return null;
            return await _context.Carts.Include(x => x.CartItems).FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<List<ViewCartItemViewModel>> GetCartItemsByUserIdAsync(string UserId)
        {
            var cartItems = await _context.CartItems
                    .Include(ci => ci.Product)  // Load thông tin sản phẩm
                    .Include(ci => ci.Variant)  // Load thông tin biến thể
                    .Where(ci => ci.Cart.UserId == UserId) // Lọc theo UserId
                    .Select(ci => new ViewCartItemViewModel
                    {
                        CartItemId = ci.Id.ToString("N"),
						productSlug = ci.Product.Slug,
						productImage = ci.Product.Image,
                        productSku = ci.Product.ProductCode,
                        variantSku = ci.Variant.SKU,
                        color = ci.Variant.Color,
                        size = ci.Variant.Size,
                        productName = ci.Product.Title,
                        quantity = ci.Quantity,
                        price = ci.Price,
                        total = ci.Price * ci.Quantity

                    })
                    .AsNoTracking()
                    .ToListAsync();

            return cartItems ?? new List<ViewCartItemViewModel>();
        }


        public async Task<decimal> GetTotalPriceAsync(string UserId)
        {
            if (string.IsNullOrEmpty(UserId)) return 0;
            var cart = await _context.Carts.FindAsync(UserId);
            if (cart == null) return 0;
            return cart.TotalPrice;
        }



        public async Task<bool> RemoveFromCartAsync(string UserId, Guid CartItemId)
        {
            try
            {
                if (string.IsNullOrEmpty(UserId)) return false;
                var cart = await _context.Carts.Include(x => x.CartItems).FirstOrDefaultAsync(x => x.UserId == UserId);
                if (cart == null || !cart.CartItems.Any()) return false;

                var cartItem = cart.CartItems.FirstOrDefault(x => x.Id == CartItemId);

                if (cartItem == null) return false;

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price * ci.Quantity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex) // Lỗi cập nhật database
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex) // Lỗi khác
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RemoveFromCart(string productSku, string variantSku)
        {
            try
            {
                var cartItem = await _context.CartItems
                     .Include(ci => ci.Product)
                     .Include(ci => ci.Variant)
                     .FirstOrDefaultAsync(ci => ci.Product.ProductCode == productSku && ci.Variant.SKU == variantSku);
                if (cartItem == null)
                {
                    return (false, "Sản phẩm không tồn tại trong giỏ hàng");
                }
                _context.CartItems.Remove(cartItem);

                // Cập nhật tổng giá của giỏ hàng
                var cart = await _context.Carts.Include(c => c.CartItems)
                                    .FirstOrDefaultAsync(c => c.Id == cartItem.CartId);


                if (cart == null)
                {
                    return (false, "Không tìm thấy giỏ hàng");
                }

                cart.TotalPrice = cart.CartItems.Any()
                       ? cart.CartItems.Sum(ci => ci.Price * ci.Quantity)
                       : 0;

                var rs = await _context.SaveChangesAsync();

                return rs > 0 ? (true, "Xoá sản phẩm khỏi giỏ hàng thành công") : (false, "Xoá sản phẩm khỏi giỏ hàng thất bại");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateCart(string productSku, string variantSku, int quantity)
        {
            try
            {
                var cartItem = await _context.CartItems
                                .Include(ci => ci.Product)
                                .Include(ci => ci.Variant)
                                .FirstOrDefaultAsync(ci => ci.Product.ProductCode == productSku && ci.Variant.SKU == variantSku);
                if (cartItem == null)
                {
                    return (false, "Sản phẩm không tồn tại trong giỏ hàng");
                }

                cartItem.Quantity = quantity;

                // Cập nhật số lượng
                cartItem.Quantity = quantity;

                // Cập nhật tổng giá của giỏ hàng
                var cart = await _context.Carts.Include(c => c.CartItems)
                                    .FirstOrDefaultAsync(c => c.Id == cartItem.CartId);

                if (cart == null)
                {
                    return (false, "Không tìm thấy giỏ hàng");
                }

                cart.TotalPrice = cart.CartItems.Any()
                       ? cart.CartItems.Sum(ci => ci.Price * ci.Quantity)
                       : 0;

                var rs = await _context.SaveChangesAsync();
                return rs > 0 ? (true, "Cập nhật thành công") : (false, "Cập nhật thất bại");

            }
            catch (Exception ex)
            {
                return (false, ex.Message.ToString());
            }
        }

        public async Task<List<ViewCartItemViewModel>> GetCartItemsByIdAsync(string UserId, List<string> cartitem)
        {
            var cartItems = await _context.CartItems
                     .Include(ci => ci.Product)  // Load thông tin sản phẩm
                     .Include(ci => ci.Variant)  // Load thông tin biến thể
                     .Where(ci => ci.Cart.UserId == UserId) // Lọc theo UserId
                     .Select(ci => new ViewCartItemViewModel
                     {
                         CartItemId = ci.Id.ToString("N"),
                         productSlug = ci.Product.Slug,
                         productImage = ci.Product.Image,
                         productId = ci.ProductId,
                         productSku = ci.Product.ProductCode,
                         variantId = ci.VariantId,
                         variantSku = ci.Variant.SKU,
                         color = ci.Variant.Color,
                         size = ci.Variant.Size,
                         productName = ci.Product.Title,
                         quantity = ci.Quantity,
                         price = ci.Price,
                         total = ci.Price * ci.Quantity

                     })
                     .AsNoTracking()
                     .ToListAsync();

            var result = cartItems.Where(x => cartitem.Contains(x.CartItemId)).ToList();
            return result;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CheckStotkItemInCart(string UserId, List<string> cartitems)
        {
            var cartItemGuids = cartitems.Select(id => Guid.Parse(id)).ToList();

            var selectedCartItems = await _context.CartItems
              .Where(ci => ci.Cart.UserId == UserId && cartItemGuids.Contains(ci.Id))
              .ToListAsync();
            if (!selectedCartItems.Any())
            {
                return (false, "Không tìm thấy sản phẩm nào trong giỏ hàng của bạn.");
            }

            var variantIds = selectedCartItems.Select(x => x.VariantId).ToList();

            var productVariants = await _context.ProductVariants
               .Where(x => variantIds.Contains(x.Id))
               .ToListAsync();

            List<string> errors = new List<string>();


            // Kiểm tra số lượng tồn kho chỉ với các sản phẩm được chọn thanh toán
            foreach (var item in selectedCartItems)
            {
                var variant = productVariants.FirstOrDefault(x => x.Id == item.VariantId);
                if (variant != null && item.Quantity > (variant.Quantity - variant.ReservedStock))
                {
                    errors.Add($"Sản phẩm {variant.Product.Title} (Màu: {variant.Color}, Size: {variant.Size}) không đủ số lượng trong kho.");
                }
            }

            if (errors.Any())
            {
                return (false, string.Join("\n", errors)); // Trả về danh sách lỗi
            }

            return (true, string.Empty); // Kiểm tra thành công
        }
    }
}
