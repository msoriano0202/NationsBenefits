using AutoMapper;
using NationsBenefits.Application.Features.Products.Commands.CreateProduct;
using NationsBenefits.Application.Features.Products.Commands.UpdateProduct;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationsBenefits.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
