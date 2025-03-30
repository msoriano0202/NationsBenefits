using MediatR;

namespace NationsBenefits.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
