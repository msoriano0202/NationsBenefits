using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Application.Specifications.SubCategories;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.SubCategories.Commands.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand>
    {
        private readonly ILogger<DeleteSubCategoryCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public DeleteSubCategoryCommandHandler(
            ILogger<DeleteSubCategoryCommandHandler> logger,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategoryToDelete = await _unitOfWork.Repository<SubCategory>().GetByIdAsync(request.Id);
            if (subCategoryToDelete == null)
            {
                _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(SubCategory), request.Id));
                throw new NotFoundException(nameof(SubCategory), request.Id);
            }

            var productsBySubCategoryParams = new ProductsBySubCategoryParams
            {
                subcategory_id = subCategoryToDelete.Id,
            };

            var specCount = new ProductsBySubCategoryCountingSpecification(productsBySubCategoryParams);
            var totalProductsBySubCategoryId = await _unitOfWork.Repository<Product>().CountAsync(specCount);
            if (totalProductsBySubCategoryId > 0)
            {
                var errorMessage = string.Format(ErrorMessages.ProductsRelatedToSubCategory, totalProductsBySubCategoryId, request.Id);
                _logger.LogError(errorMessage);
                throw new BadRequestException(errorMessage);
            }

            _unitOfWork.Repository<SubCategory>().DeleteEntity(subCategoryToDelete);

            await _unitOfWork.Complete();

            _cacheService.RemoveData(RedisValues.SubCategoriesKey);
            _cacheService.RemoveData($"{RedisValues.SubCategoriesKey}_{request.Id}");

            _logger.LogInformation(string.Format(SuccessMessages.EntityDeleted, nameof(SubCategory), request.Id));
        }
    }
}
