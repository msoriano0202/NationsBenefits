using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Application.Features.Products.Queries.GetProductById;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, SubCategoryDto>
    {
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubCategoryByIdQueryHandler(
            ILogger<GetProductByIdQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubCategoryDto> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var subCategory = await _unitOfWork.Repository<SubCategory>().GetByIdAsync(request.Id);
            if (subCategory == null)
            {
                _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(SubCategory), request.Id));
                throw new NotFoundException(nameof(SubCategory), request.Id);
            }

            return _mapper.Map<SubCategoryDto>(subCategory);
        }
    }
}
