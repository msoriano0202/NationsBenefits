using NationsBenefits.Domain.Common;

namespace NationsBenefits.Domain
{
    public class SubCategory : BaseDomainModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int Category_Id { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
