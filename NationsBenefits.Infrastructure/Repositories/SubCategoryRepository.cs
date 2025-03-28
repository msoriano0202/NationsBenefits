using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Domain;
using NationsBenefits.Infrastructure.Persistence;

namespace NationsBenefits.Infrastructure.Repositories
{
    public class SubCategoryRepository : RepositoryBase<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(NationsBenefitsDbContext context) : base(context)
        {
        }
    }
}
