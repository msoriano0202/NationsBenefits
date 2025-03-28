using NationsBenefits.Domain;

namespace NationsBenefits.Application.Specifications.Products
{
    public class ProductCountingSpecification : BaseSpecification<Product>
    {
        public ProductCountingSpecification(ProductSpecificationParams productParams)
            : base(x => string.IsNullOrEmpty(productParams.Name) || x.Name!.Contains(productParams.Name))
        {
            
        }
    }
}
