using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.SubCategories.Commands.BulkInsertSubCategory
{
    public class BulkInsertSubCategoryCommandHandler : IRequestHandler<BulkInsertSubCategoryCommand, bool>
    {
        private readonly ILogger<BulkInsertSubCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BulkInsertSubCategoryCommandHandler(
            ILogger<BulkInsertSubCategoryCommandHandler> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(BulkInsertSubCategoryCommand request, CancellationToken cancellationToken)
        {
            try {
                var dataToInsert = _mapper.Map<List<SubCategory>>(request.Data);
                dataToInsert.ForEach(x => {
                    x.CreatedAt = DateTime.Now;
                    x.UpdatedAt = DateTime.Now;
                });
                await _unitOfWork.Repository<SubCategory>().BulkInsertAsync(dataToInsert);
                return true;
            }
            catch (Exception e)
            {
                var errorMessage = string.Format(ErrorMessages.BulkInsertError, e.Message);
                _logger.LogError(errorMessage);
                throw new BadRequestException(errorMessage);
            }
        }
    }
}
