using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerViewComponent(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _bannerRepository.GetBannersShowAsync();
            model = model.OrderBy(x => x.Type).ToList();
            return View(model);
        }
    }
}
