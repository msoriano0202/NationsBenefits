using NationsBenefits.Domain;

namespace NationsBenefits.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<Product> GetProductByName(string name);
    }
}
