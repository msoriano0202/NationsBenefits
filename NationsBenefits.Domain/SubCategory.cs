using NationsBenefits.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationsBenefits.Domain
{
    public class SubCategory : BaseDomainModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("category_id")]
        public int CategoryId { get; set; }

        [NotMapped]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
