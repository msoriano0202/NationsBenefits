using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, int>
    {
        private readonly ILogger<CreateSubCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public CreateSubCategoryCommandHandler(
            ILogger<CreateSubCategoryCommandHandler> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<int> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategoryEntity = _mapper.Map<SubCategory>(request);
            _unitOfWork.Repository<SubCategory>().AddEntity(subCategoryEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                var errorMessage = string.Format(ErrorMessages.EntityNotInserted, nameof(SubCategory));
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            _cacheService.RemoveData(RedisValues.SubCategoriesKey);

            return subCategoryEntity.Id;
        }
    }
}
