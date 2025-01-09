using AutoMapper;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Product;

namespace Coza_Ecommerce_Shop.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product -> ProductDeatilInfoViewModel
            CreateMap<Product, ProductDetailInfoViewModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Title ?? string.Empty))
            .ReverseMap();
            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
            CreateMap<ProductVariant, ProductVariantsViewModel>().ReverseMap();

        }
    }
}
