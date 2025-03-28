using MediatR;
using NationsBenefits.Application.Models;

namespace NationsBenefits.Application.Features.SubCategories.Queries
{
    public class GetSubCategoriesQuery : IRequest<IReadOnlyList<SubCategoryDto>>
    {
    }
}
