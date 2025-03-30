using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Infrastructure.Persistence;
using NationsBenefits.Infrastructure.Repositories;

namespace NationsBenefits.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<NationsBenefitsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NationsBenefitsConn"))
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

    }
}
