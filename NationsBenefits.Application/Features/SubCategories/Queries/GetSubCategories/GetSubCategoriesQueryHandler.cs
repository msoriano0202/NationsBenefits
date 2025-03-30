using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategories
{
    public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, IReadOnlyList<SubCategoryDto>>
    {
        private readonly ILogger<GetSubCategoriesQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetSubCategoriesQueryHandler(
            ILogger<GetSubCategoriesQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<IReadOnlyList<SubCategoryDto>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = _cacheService.GetData<IReadOnlyList<SubCategory>>(RedisValues.SubCategoriesKey);
            if (subCategories == null)
            {
                subCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsync();

                var expirationTime = DateTime.Now.AddMinutes(RedisValues.CacheTimeInMinutes);
                _cacheService.SetData<IReadOnlyList<SubCategory>>(RedisValues.SubCategoriesKey, subCategories, expirationTime);
            }

            return _mapper.Map<IReadOnlyList<SubCategoryDto>>(subCategories);
        }
    }
}
