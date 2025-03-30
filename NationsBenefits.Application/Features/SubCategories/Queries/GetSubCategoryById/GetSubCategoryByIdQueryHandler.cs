using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Application.Features.Products.Queries.GetProductById;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, SubCategoryDto>
    {
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetSubCategoryByIdQueryHandler(
            ILogger<GetProductByIdQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<SubCategoryDto> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            SubCategory subCategory = null;

            var subCategories = _cacheService.GetData<IReadOnlyList<SubCategory>>(RedisValues.SubCategoriesKey);
            if (subCategories == null)
            {
                var redisKey = $"{RedisValues.SubCategoriesKey}_{request.Id}";
                subCategory = _cacheService.GetData<SubCategory>(redisKey);
                if (subCategory == null)
                {
                    subCategory = await _unitOfWork.Repository<SubCategory>().GetByIdAsync(request.Id);
                    if (subCategory != null)
                    {
                        var expirationTime = DateTime.Now.AddMinutes(RedisValues.CacheTimeInMinutes);
                        _cacheService.SetData<SubCategory>(redisKey, subCategory, expirationTime);
                    }
                }
            }
            else 
            {
                subCategory = subCategories.FirstOrDefault(s => s.Id == request.Id);
            }

            if (subCategory == null)
            {
                _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(SubCategory), request.Id));
                throw new NotFoundException(nameof(SubCategory), request.Id);
            }

            return _mapper.Map<SubCategoryDto>(subCategory);
        }
    }
}
