using NationsBenefits.Domain;

namespace NationsBenefits.Application.Specifications.SubCategories
{
    public class ProductsBySubCategoryCountingSpecification : BaseSpecification<Product>
    {
        public ProductsBySubCategoryCountingSpecification(ProductsBySubCategoryParams productParams)
           : base(x => x.Subcategory_id == productParams.subcategory_id)
        {

        }
    }
}
