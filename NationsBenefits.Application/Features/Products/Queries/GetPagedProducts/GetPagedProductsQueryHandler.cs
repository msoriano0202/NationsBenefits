using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Models;
using NationsBenefits.Application.Models.Shared;
using NationsBenefits.Application.Specifications.Products;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, PagedResponse<ProductDto>>
    {
        private readonly ILogger<GetPagedProductsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetPagedProductsQueryHandler(
            ILogger<GetPagedProductsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"{RedisValues.ProductsKey}_{request.Page}_{request.PageSize}_{request.Name}";

            var pagedProducts = _cacheService.GetData<PagedResponse<ProductDto>>(redisKey);
            if (pagedProducts == null)
            {
                var productSpecificationParams = new ProductSpecificationParams
                {
                    Name = request.Name,
                    Page = request.Page,
                    PageSize = request.PageSize
                };

                var specCount = new ProductCountingSpecification(productSpecificationParams);
                var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(specCount);

                var spec = new ProductSpecification(productSpecificationParams);
                var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);

                var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(productSpecificationParams.PageSize));
                var totalPages = Convert.ToInt32(rounded);

                var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

                pagedProducts = new PagedResponse<ProductDto>
                {
                    TotalCount = totalProducts,
                    Items = data,
                    TotalPages = totalPages,
                    CurrentPage = productSpecificationParams.Page,
                    PageSize = productSpecificationParams.PageSize
                };

                var expirationTime = DateTime.Now.AddMinutes(RedisValues.CacheTimeInMinutes);
                _cacheService.SetData<PagedResponse<ProductDto>>(redisKey, pagedProducts, expirationTime);
            }

            return pagedProducts;
        }
    }
}
