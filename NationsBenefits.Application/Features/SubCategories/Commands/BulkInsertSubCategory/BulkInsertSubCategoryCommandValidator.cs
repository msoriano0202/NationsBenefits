using FluentValidation;

namespace NationsBenefits.Application.Features.SubCategories.Commands.BulkInsertSubCategory
{
    public class BulkInsertSubCategoryCommandValidator : AbstractValidator<BulkInsertSubCategoryCommand>
    {
        public BulkInsertSubCategoryCommandValidator()
        {
            RuleFor(x => x.Data).Custom((list, context) => {
                foreach (var item in list) {
                    if (item.CategoryId <= 0) {
                        context.AddFailure("{Category_Id} has to be greater than zero");
                    }
                    if (item.Code == null)
                    {
                        context.AddFailure("{Code} can not be null");
                    }
                    else if (item.Code.Length > 50)
                    {
                        context.AddFailure("{Code} exceed lenght");
                    }

                    if (item.Description == null) 
                    {
                        context.AddFailure("{Description} can not be null");
                    }
                    else if (item.Description.Length > 250)
                    {
                        context.AddFailure("{Description} exceed lenght");
                    }
                }
            });
        }
    }
}
