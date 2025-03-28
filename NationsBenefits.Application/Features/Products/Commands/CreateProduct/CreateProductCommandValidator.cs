using FluentValidation;

namespace NationsBenefits.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Subcategory_id)
               .GreaterThan(0).WithMessage("{Subcategory_id} has to be greater than zero");

            RuleFor(p => p.Ski)
               .NotNull().WithMessage("{Ski} can not be null");

            RuleFor(p => p.Name)
                .NotNull().WithMessage("{Name} can not be null");
        }
    }
}
