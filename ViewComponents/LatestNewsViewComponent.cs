using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class LatestNewsViewComponent : ViewComponent
    {
        private readonly INewRepository _newRepository;

        public LatestNewsViewComponent(INewRepository newRepository)
        {
            _newRepository = newRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(New model)
        {
            var listnews = await _newRepository.GetAllAsync();
            var result = listnews.Where(x => x.Slug != model.Slug.ToLower()).OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(result);
        }
    }
}
