using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Extentions;
using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        public INotyfService _notifyService { get; }

        public EmployeeController(IAccountRepository accountRepository, IRoleRepository roleRepository, INotyfService notifyService)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _notifyService = notifyService;
        }
        [HttpGet("")]
        [HttpGet("Index")]
        [Authorize(Policy = "ViewUsers")]
        public async Task<IActionResult> Index(string search, string filter = "All", int? page = 1)
        {
            
            var listfilter = new SelectListItem[]
            {
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "Employee", Value = "Employee" }
            };
            ViewBag.filters = new SelectList(listfilter, "Value", "Text", filter);

            int pageSize = 10;
            int pageNumber = page ?? 1;
            var employees = await _accountRepository.GetAllEmployeesAsync(filter);
            

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(x => x.UserName.ToLower().Contains(search.ToLower()) || (x.FullName != null && x.Email.ToLower().Contains(search.ToLower())) || x.PhoneNumber.Contains(search));
            }

            
            var totalData = employees.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;


            pageNumber = pageNumber > totalPages ? 1 : pageNumber;

            var pagedList = employees.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);

            var employeePagingViewModel = new EmployeePagingViewModel
            {
                listEmployees = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };

            return View(employeePagingViewModel);
        }

        [HttpGet("Details/{id}")]
        [Authorize(Policy = "ViewUserDetail")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _accountRepository.GetEmployeebyId(id);

            return View(user);
        }


        [HttpGet("Create")]
        [Authorize(Policy = "CreateUser")]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleRepository.GetAllRolesAdminPageAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }
        
        [HttpPost("Create")]
        [Authorize(Policy = "CreateUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeeViewModel employeeViewModel)
        {
            var roles = await _roleRepository.GetAllRolesAdminPageAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name", employeeViewModel.Role);

            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _accountRepository.CreateAccountEmployee(employeeViewModel);
                if (isSuccess)
                {
                    _notifyService.Success(errorMessage);
                    return RedirectToAction("Index");
                }
                _notifyService.Error(errorMessage);
            }

            return View(employeeViewModel);
        }

        [HttpGet("Edit/{id}")]
        [Authorize(Policy = "EditUser")]
        public async Task<IActionResult> Edit(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _accountRepository.GetEmployeebyId(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleRepository.GetAllRolesAdminPageAsync();
            var IdRoleSelected = roles.FirstOrDefault(r => r.Name == user.Role)?.Id;
            user.Role = IdRoleSelected;
            ViewBag.Roles = new SelectList(roles, "Id", "Name", IdRoleSelected);
            return View(user);
        }

        [HttpPost("Edit/{id}")]
        [Authorize(Policy = "EditUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] EmployeeViewModel employeeViewModel)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }


            var user = await _accountRepository.GetEmployeebyId(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleRepository.GetAllRolesAdminPageAsync();
            var IdRoleSelected = roles.FirstOrDefault(r => r.Name == user.Role)?.Id;
            user.Role = IdRoleSelected;
            ViewBag.Roles = new SelectList(roles, "Id", "Name", IdRoleSelected);


            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _accountRepository.UpdateAccountEmployee(employeeViewModel);

                if (isSuccess)
                {
                    _notifyService.Success(errorMessage);
                    return RedirectToAction("Index");
                }
                _notifyService.Error(errorMessage);

            }
            return View(employeeViewModel);
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Policy = "DeleteUser")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var (isSuccess, errorMessage) = await _accountRepository.DeleteAccountEmployee(id);
            if (isSuccess)
            {
                _notifyService.Success(errorMessage);
                return RedirectToAction("Index");
            }
            _notifyService.Error(errorMessage);
            return RedirectToAction("Index");
        }


        
    }
}
