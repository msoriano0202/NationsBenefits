using MediatR;

namespace NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommand : IRequest<int>
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
