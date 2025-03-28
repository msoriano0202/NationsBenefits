using NationsBenefits.Domain;

namespace NationsBenefits.Application.Specifications.Products
{
    public  class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams productParams)
            : base(x => string.IsNullOrEmpty(productParams.Name) || x.Name!.Contains(productParams.Name))
        {
            ApplyPaging(productParams.PageSize * (productParams.Page - 1), productParams.PageSize);
        }
    }
}
