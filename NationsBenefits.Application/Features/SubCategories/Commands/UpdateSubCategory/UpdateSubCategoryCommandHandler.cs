using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Application.Features.Products.Commands.UpdateProduct;
using NationsBenefits.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationsBenefits.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, int>
    {
        private readonly ILogger<UpdateSubCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSubCategoryCommandHandler(
          ILogger<UpdateSubCategoryCommandHandler> logger,
          IMapper mapper,
          IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategoryToUpdate = await _unitOfWork.Repository<SubCategory>().GetByIdAsync(request.Id);
            if (subcategoryToUpdate == null)
            {
                _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(SubCategory), request.Id));
                throw new NotFoundException(nameof(SubCategory), request.Id);
            }

            _mapper.Map(request, subcategoryToUpdate, typeof(UpdateSubCategoryCommand), typeof(SubCategory));

            _unitOfWork.Repository<SubCategory>().UpdateEntity(subcategoryToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation(string.Format(SuccessMessages.EntityUpdated, nameof(SubCategory), request.Id));

            return request.Id;
        }
    }
}
