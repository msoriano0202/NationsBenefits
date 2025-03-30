using FluentValidation;

namespace NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
    {
        public CreateSubCategoryCommandValidator()
        {
            RuleFor(p => p.CategoryId)
               .GreaterThan(0).WithMessage("{Category_Id} has to be greater than zero");

            RuleFor(p => p.Code)
               .NotNull().WithMessage("{Code} can not be null")
               .MaximumLength(50).WithMessage("{Code} exceed lenght");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("{Description} can not be null")
                .MaximumLength(250).WithMessage("{Code} exceed lenght");
        }
    }
}
