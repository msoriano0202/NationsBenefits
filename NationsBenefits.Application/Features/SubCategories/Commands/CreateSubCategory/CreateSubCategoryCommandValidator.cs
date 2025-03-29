using FluentValidation;

namespace NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
    {
        public CreateSubCategoryCommandValidator()
        {
            RuleFor(p => p.Category_Id)
               .GreaterThan(0).WithMessage("{Category_Id} has to be greater than zero");

            RuleFor(p => p.Code)
               .NotNull().WithMessage("{Code} can not be null");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("{Description} can not be null");
        }
    }
}
