using Coza_Ecommerce_Shop.Repositories.Implementations;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/contact")]
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string search, int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = await _contactRepository.GetContacts();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search) || x.Email.Contains(search));
            }

            var totalData = query.Count();

            var totalPages = totalData > 0 ? (int)Math.Ceiling((double)totalData / pageSize) : 1;


            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            var pagedList = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);
           
            var ContactsViewModel = new ContactPagingViewModel
            {
                contacts = pagedList,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalCount = pagedList.Count,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    SearchTerm = search,
                }
            };

            return View(ContactsViewModel);
        }

        [HttpPost("load-detail-message")]
        public async Task<IActionResult> LoadDetailMessage([FromBody] LoadMessageRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Id))
            {
                return BadRequest("ID không hợp lệ");
            }

            var contact = await _contactRepository.GetContactById(request?.Id);
            if(contact == null)
            {
                return NotFound("Không tìm thấy gì");
            }
            return Ok(new {data = contact });
        }
    }


    public class LoadMessageRequest
    {
        public string Id { get; set; }
    }
}
