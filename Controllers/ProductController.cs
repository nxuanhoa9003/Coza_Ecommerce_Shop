using AutoMapper;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.Models.Entities;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IProductCategoryService productCategoryService, IProductService productService, UserService userService, IMapper mapper)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productCategoryService = productCategoryService;
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("product-detail/{slug}")]
        public async Task<IActionResult> ProductDetail(string slug)
        {
            var product = await _productRepository.GetDetailProductBySlugAsync(slug);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        [HttpGet("list-product")]
        public async Task<IActionResult> ListProduct([FromQuery] ParamsFilter filter)
        {
            var breadCrumbShow = "Sản phẩm";
            var productsListViewModel = new ProductsListViewModel();

            if (!string.IsNullOrEmpty(filter.slug))
            {
                var productCategory = await _productCategoryRepository.GetBySlugAsNoTrackingAsync(filter.slug);
                if (productCategory != null)
                {
                    breadCrumbShow = productCategory.Title;
                }
            }
            productsListViewModel.breadCrumbShow = breadCrumbShow;
            productsListViewModel.paramsFilter = filter;

            return View(productsListViewModel);
        }

        [HttpGet("load-more-product")]
        public async Task<IActionResult> LoadMore(int? page = 1)
        {
            ++page;
            int pageSize = 4;

            var data = await _productService.GetProductByPage(page.Value, pageSize);
            return Json(new
            {
                products = data,
                nextPage = page
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            });

        }


        [HttpGet("GetVariant")]
        public async Task<IActionResult> GetVariant(string sku, string color, string size)
        {
            var product = await _productRepository.GetDetailProductBySKUAsync(sku);
            if (product == null)
            {
                return NotFound();
            }

            var selectedVariant = product.Variants
                                .FirstOrDefault(v => v.Color == color && v.Size == size);
            if (selectedVariant == null)
            {
                return NotFound("Variant không tồn tại");
            }

            var ObjVariant = _mapper.Map<ProductVariantsViewModel>(selectedVariant);


            return Json(new
            {
                variant = ObjVariant
            });

        }

        [HttpGet("get-variants")]
        public async Task<IActionResult> GetVariantsForProduct(string sku)
        {
            var product = await _productRepository.GetDetailProductBySKUAsync(sku);
            if (product == null)
            {
                return NotFound();
            }

            var selectedVariant = product.Variants;
            if (selectedVariant == null)
            {
                return NotFound("Variant không tồn tại");
            }

            var variants = product.Variants.Select(v => new
            {
                size = v.Size,
                color = v.Color,
                stock = v.IsActive ? v.Quantity : 0
            }).ToList();

            return Json(new { variants });
        }

        [HttpPost("load-wishlist")]
        public async Task<IActionResult> LoadWishlist()
        {
            var userPrincipal = await _userService.GetUserByUserTypeAsync("Customer");
            if (userPrincipal == null)
            {
                return BadRequest(new { isLogin = false, message = "Chưa đăng nhập" });
            }
            var userid = userPrincipal.FindFirst("Id").Value;
            var rs = await _productRepository.GetWishlistByUserid(userid.ToString());
            return Ok(new { issuccess = true, data = rs });
        }

        [HttpPost("add-to-wishlist")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistRequestViewModel model)
        {
            var userPrincipal = await _userService.GetUserByUserTypeAsync("Customer");
            if (userPrincipal == null)
            {
                return BadRequest(new { isLogin = false, message = "Chưa đăng nhập" });
            }
            if (model.productid == Guid.Empty)
            {
                return BadRequest(new { isLogin = true, message = "Thêm thất bại" });
            }

            var userid = userPrincipal.FindFirst("Id").Value;
            Wishlist wishlist = new Wishlist
            {
                ProductId = model.productid,
                UserId = userid.ToString()
            };
            var (IsSuccess, ErrorMessage) = await _productRepository.AddToWishlist(wishlist);
            return Ok(new { isAdded = IsSuccess, message = IsSuccess ? "Đã thêm vào danh sách yêu thích" : ErrorMessage });

        }

        [HttpGet("my-wishlist")]
        public async Task<IActionResult> MyWishlist()
        {
            var userPrincipal = await _userService.GetUserByUserTypeAsync("Customer");
            if (userPrincipal == null)
            {
                return RedirectToAction("login", "account");
            }
            var userid = userPrincipal.FindFirst("Id").Value;
            var listProductWishtlist = await _productRepository.GetProductWishlistByUserid(userid.ToString());
            
            return View(listProductWishtlist);
        }

    }
}
