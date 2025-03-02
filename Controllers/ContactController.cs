using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Services;
using Coza_Ecommerce_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Coza_Ecommerce_Shop.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserService _userService;

        public ContactController(IContactRepository contactRepository, UserService userService)
        {
            _contactRepository = contactRepository;
            _userService = userService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] ContactViewModel model)
        {
            var userPrincipal = await _userService.GetUserByUserTypeAsync("Customer");

            if (userPrincipal == null)
            {
                if (string.IsNullOrEmpty(model.email))
                {
                    return BadRequest("Chưa nhập email");
                }
                if (string.IsNullOrEmpty(model.fullname))
                {
                    return BadRequest("Chưa nhập tên người gửi");

                }
            }else
            {
                var email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;
                var fullname = userPrincipal.FindFirst("fullname")?.Value;
                model.email = email;
                model.fullname = fullname;
            }

            if (string.IsNullOrEmpty(model.message))
            {
                return BadRequest("Chưa nhập tin nhắn đã gửi");
            }

            Contact contact = new Contact
            {
                Email = model.email,
                Name = model.fullname,
                Message = model.message
            };

            var (IsSuccess, ErrorMessage) = await _contactRepository.CreateAsync(contact);
            if (IsSuccess)
            {
                return Ok(ErrorMessage);
            }
            else
            {
                return BadRequest(ErrorMessage);
            }
            
        }
    }
}
