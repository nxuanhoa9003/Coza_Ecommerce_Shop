using Coza_Ecommerce_Shop.ViewModels.Account;
using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using X.PagedList.Extensions;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class CustomerController : Controller
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        public INotyfService _notifyService { get; }

        public CustomerController(IAccountRepository accountRepository, IRoleRepository roleRepository, INotyfService notifyService)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _notifyService = notifyService;
        }

        [HttpGet("list-customer")]
        [Authorize(Policy = "ViewListCustomer")]
        public async Task<IActionResult> ListCustomers(string search, int? page = 1)
        {

            int pageSize = 10;
            int pageNumber = page ?? 1;
            var customers = await _accountRepository.GetAllCustomersAsync("Customer");

            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(x => x.UserName.ToLower().Contains(search.ToLower()) || (x.FullName != null && x.Email.ToLower().Contains(search.ToLower())) || x.PhoneNumber.Contains(search));
            }


            var totalData = customers.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;


            pageNumber = pageNumber > totalPages ? 1 : pageNumber;

            var pagedList = customers.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var customerPagingViewModel = new CustomerPagingViewModel
            {
                listCustomers = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };
            return View(customerPagingViewModel);
        }

        [HttpPost("detail-customer")]
        [Authorize(Policy = "ViewDetailListCustomer")]
        public async Task<IActionResult> DetailCustomer([FromBody] LoadCustomerRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Id))
            {
                return BadRequest("ID không hợp lệ");
            }

            var customer = await _accountRepository.GetDetailCustomersAsync(request.Id);

            if (customer == null)
            {
                return NotFound("Không tìm thấy khách hàng");
            }
            return Ok(new { data = customer });
        }

    }

    public class LoadCustomerRequest
    {
        public string Id { get; set; }
    }
}
