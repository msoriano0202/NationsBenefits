using MediatR;
using NationsBenefits.Application.Models;

namespace NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQuery : IRequest<SubCategoryDto>
    {
        public int Id { get; set; }
        public GetSubCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
