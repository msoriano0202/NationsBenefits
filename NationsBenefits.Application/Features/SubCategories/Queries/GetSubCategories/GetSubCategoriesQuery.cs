using MediatR;
using NationsBenefits.Application.Models;

namespace NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategories
{
    public class GetSubCategoriesQuery : IRequest<IReadOnlyList<SubCategoryDto>>
    {
    }
}
