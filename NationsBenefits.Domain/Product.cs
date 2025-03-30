using NationsBenefits.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationsBenefits.Domain
{
    public class Product: BaseDomainModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("subcategory_id")]
        public int SubcategoryId { get; set; }

        [Column("ski")]
        public string Ski { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public virtual SubCategory? SubCategory { get; set; }
    }
}
