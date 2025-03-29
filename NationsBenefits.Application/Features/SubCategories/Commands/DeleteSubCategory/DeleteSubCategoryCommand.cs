using MediatR;

namespace NationsBenefits.Application.Features.SubCategories.Commands.DeleteSubCategory
{
    public  class DeleteSubCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteSubCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
