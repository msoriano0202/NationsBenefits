using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Domain.Common;
using NationsBenefits.Infrastructure.Persistence;
using System.Collections;

namespace NationsBenefits.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;


        private readonly NationsBenefitsDbContext _context;
        public NationsBenefitsDbContext NationsBenefitsDbContext => _context;

        public UnitOfWork(NationsBenefitsDbContext context)
        {
            _context = context;
        }


        private IProductRepository _productRepository;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        private ISubCategoryRepository _subCategoryRepository;
        public ISubCategoryRepository SubCategoryRepository => _subCategoryRepository ??= new SubCategoryRepository(_context);


        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
