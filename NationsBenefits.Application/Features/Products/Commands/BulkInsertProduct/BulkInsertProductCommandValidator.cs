using FluentValidation;

namespace NationsBenefits.Application.Features.Products.Commands.BulkInsertProduct
{
    public class BulkInsertProductCommandValidator : AbstractValidator<BulkInsertProductCommand>
    {
        public BulkInsertProductCommandValidator()
        {
            RuleFor(x => x.Data).Custom((list, context) =>
            {
                foreach (var item in list)
                {
                    if (item.SubcategoryId <= 0)
                    {
                        context.AddFailure("{SubcategoryId} has to be greater than zero");
                    }
                    if (item.Ski == null)
                    {
                        context.AddFailure("{Ski} can not be null");
                    }
                    else if (item.Ski.Length > 15)
                    {
                        context.AddFailure("{Ski} exceed lenght");
                    }

                    if (item.Name == null)
                    {
                        context.AddFailure("{Name} can not be null");
                    }
                    else if (item.Name.Length > 150)
                    {
                        context.AddFailure("{Name} exceed lenght");
                    }
                }
            });
        }
    }
}
