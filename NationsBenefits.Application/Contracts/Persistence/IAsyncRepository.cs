using NationsBenefits.Application.Specifications;
using NationsBenefits.Domain.Common;
using System.Linq.Expressions;

namespace NationsBenefits.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T: BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsByIdAsync(int id);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
