using NationsBenefits.Domain.Common;
using System.IO;

namespace NationsBenefits.Domain
{
    public class Product: BaseDomainModel
    {
        public int Id { get; set; }
        public int Subcategory_id { get; set; }
        public string? Ski { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual SubCategory? SubCategory { get; set; }
    }
}
