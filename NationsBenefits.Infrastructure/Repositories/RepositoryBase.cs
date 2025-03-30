using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Specifications;
using NationsBenefits.Domain.Common;
using NationsBenefits.Infrastructure.Persistence;
using NationsBenefits.Infrastructure.Specification;
using System.Linq.Expressions;

namespace NationsBenefits.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly NationsBenefitsDbContext _context;

        public RepositoryBase(NationsBenefitsDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return (await _context.Set<T>().ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return (await _context.Set<T>().Where(predicate).ToListAsync()).AsReadOnly();
        }


        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }


        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return (await ApplySpecification(spec).ToListAsync()).AsReadOnly();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task BulkInsertAsync(IEnumerable<T> buklData)
        {
            await _context.BulkInsertAsync<T>(buklData);
        }
    }
}
