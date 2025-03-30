using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Commands.BulkInsertProduct
{
    public class BulkInsertProductCommandHandler : IRequestHandler<BulkInsertProductCommand, bool>
    {
        private readonly ILogger<BulkInsertProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BulkInsertProductCommandHandler(
            ILogger<BulkInsertProductCommandHandler> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(BulkInsertProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dataToInsert = _mapper.Map<List<Product>>(request.Data);
                dataToInsert.ForEach(x => {
                    x.CreatedAt = DateTime.Now;
                    x.UpdatedAt = DateTime.Now;
                });
                await _unitOfWork.Repository<Product>().BulkInsertAsync(dataToInsert);
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
