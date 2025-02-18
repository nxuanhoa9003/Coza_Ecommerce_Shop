using AutoMapper;
using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.ViewModels;
using Coza_Ecommerce_Shop.ViewModels.Product;
using Microsoft.CodeAnalysis.Options;

namespace Coza_Ecommerce_Shop.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<ProductViewModel, Product>()
             .ForMember(dest => dest.Files, opt => opt.Ignore()) // Không map IFormFile
             .ForMember(dest => dest.Variants, opt => opt.MapFrom(src => src.Variants))
             .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages))
             .ReverseMap();

            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
            CreateMap<ProductVariant, ProductVariantsViewModel>().ReverseMap();

        }
    }
}
