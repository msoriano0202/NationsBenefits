using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(
            ILogger<CreateProductCommandHandler> logger, 
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var subCategoryExist = await _unitOfWork.Repository<SubCategory>().GetByIdAsync(request.SubcategoryId);
            if (subCategoryExist == null)
            {
                var erroMessage = string.Format(ErrorMessages.EntityNotExists, nameof(SubCategory), request.SubcategoryId);
                _logger.LogError(erroMessage);
                throw new BadRequestException(erroMessage);
            }

            var productEntity = _mapper.Map<Product>(request);
            _unitOfWork.Repository<Product>().AddEntity(productEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                var errorMessage = string.Format(ErrorMessages.EntityNotInserted, nameof(Product));
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            _logger.LogInformation(string.Format(SuccessMessages.EntityInserted, nameof(Product)));
            return productEntity.Id;
        }
    }
}
