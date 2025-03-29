using AutoMapper;
using NationsBenefits.Application.Features.Products.Commands.CreateProduct;
using NationsBenefits.Application.Features.Products.Commands.UpdateProduct;
using NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory;
using NationsBenefits.Application.Features.SubCategories.Commands.UpdateSubCategory;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            CreateMap<CreateSubCategoryCommand, SubCategory>();
            CreateMap<UpdateSubCategoryCommand, SubCategory>();
        }
    }
}
