using AutoMapper;
using MediatR;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.SubCategories.Queries
{
    public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, IReadOnlyList<SubCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubCategoriesQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<SubCategoryDto>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsync();

            return _mapper.Map<IReadOnlyList<SubCategoryDto>>(subCategories);
        }
    }
}
