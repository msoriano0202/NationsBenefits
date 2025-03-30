using MediatR;
using NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory;

namespace NationsBenefits.Application.Features.SubCategories.Commands.BulkInsertSubCategory
{
    public class BulkInsertSubCategoryCommand : IRequest<bool>
    {
        public List<CreateSubCategoryCommand> Data { get; set; } = new List<CreateSubCategoryCommand>();
    }
}
