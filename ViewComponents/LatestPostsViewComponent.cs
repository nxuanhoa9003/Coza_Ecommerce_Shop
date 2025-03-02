using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class LatestPostsViewComponent : ViewComponent
    {
        private readonly IPostRepository _postRepository;

        public LatestPostsViewComponent(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(Post model)
        {
            var listnews = await _postRepository.GetAllAsync();
            var result = listnews.Where(x => x.Slug != model.Slug.ToLower()).OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(result);
        }
    }
}
