using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(
            ILogger<DeleteProductCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (productToDelete == null)
            {
                _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(Product), request.Id));
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _unitOfWork.Repository<Product>().DeleteEntity(productToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation(string.Format(SuccessMessages.EntityDeleted, nameof(Product), request.Id));
        }
    }
}
