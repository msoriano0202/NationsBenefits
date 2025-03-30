using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            ILogger<UpdateProductCommandHandler> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var subCategoryExist = await _unitOfWork.Repository<SubCategory>().GetByIdAsync(request.SubcategoryId);
            if (subCategoryExist == null)
            {
                var errorMessage = string.Format(ErrorMessages.EntityNotExists, nameof(SubCategory), request.SubcategoryId);
                _logger.LogError(errorMessage);
                throw new BadRequestException(errorMessage);
            }

            var productToUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (productToUpdate == null)
            {
                _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(Product), request.Id));
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));

            _unitOfWork.Repository<Product>().UpdateEntity(productToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation(string.Format(SuccessMessages.EntityUpdated, nameof(Product), request.Id));

            return request.Id;
        }
    }
}
