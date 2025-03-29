using AutoMapper;
using MediatR;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Models;
using NationsBenefits.Application.Models.Shared;
using NationsBenefits.Application.Specifications.Products;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, PagedResponse<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPagedProductsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var productSpecificationParams = new ProductSpecificationParams
            {
                Name = request.Name,
                Page = request.Page,
                PageSize = request.PageSize
            };

            var spec = new ProductSpecification(productSpecificationParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);

            var specCount = new ProductCountingSpecification(productSpecificationParams);
            var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(productSpecificationParams.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            var pagination = new PagedResponse<ProductDto>
            {
                TotalCount = totalProducts,
                Items = data,
                TotalPages = totalPages,
                CurrentPage = productSpecificationParams.Page,
                PageSize = productSpecificationParams.PageSize
            };

            return pagination;
        }
    }
}
