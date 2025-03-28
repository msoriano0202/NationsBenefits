using NationsBenefits.Domain.Common;

namespace NationsBenefits.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISubCategoryRepository SubCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        Task<int> Complete();
    }
}
