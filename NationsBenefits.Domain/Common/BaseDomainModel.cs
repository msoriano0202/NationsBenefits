using System.ComponentModel.DataAnnotations.Schema;

namespace NationsBenefits.Domain.Common
{
    public abstract class BaseDomainModel
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
