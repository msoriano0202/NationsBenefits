using Microsoft.EntityFrameworkCore;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Domain;
using NationsBenefits.Infrastructure.Persistence;

namespace NationsBenefits.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NationsBenefitsDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products!.Where(p => p.Name.Contains(name)).FirstOrDefaultAsync();
        }
    }
}
