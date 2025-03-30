using FluentValidation;

namespace NationsBenefits.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.SubcategoryId)
               .GreaterThan(0).WithMessage("{Subcategory_id} has to be greater than zero");

            RuleFor(p => p.Ski)
               .NotNull().WithMessage("{Ski} can not be null")
               .MaximumLength(15).WithMessage("{Ski} exceed lenght");

            RuleFor(p => p.Name)
                .NotNull().WithMessage("{Name} can not be null")
                .MaximumLength(150).WithMessage("{Name} exceed lenght");
        }
    }
}
