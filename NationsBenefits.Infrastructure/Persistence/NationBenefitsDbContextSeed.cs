using Microsoft.Extensions.Logging;
using NationsBenefits.Domain;

namespace NationsBenefits.Infrastructure.Persistence
{
    public class NationBenefitsDbContextSeed
    {
        public static async Task SeedAsync(NationsBenefitsDbContext context, ILoggerFactory loggerFactory)
        {
            if (!context.SubCategories.Any())
            {
                var logger = loggerFactory.CreateLogger<NationsBenefitsDbContext>();
                context.SubCategories!.AddRange(GetPreconfiguredSubCategories());
                await context.SaveChangesAsync();
                logger.LogInformation("Inserting new records into db {context}", typeof(NationsBenefitsDbContext).Name);
            }

            if (!context.Products.Any())
            {
                var logger = loggerFactory.CreateLogger<NationsBenefitsDbContext>();
                context.Products!.AddRange(GetPreconfiguredProducts());
                await context.SaveChangesAsync();
                logger.LogInformation("Inserting new records into db {context}", typeof(NationsBenefitsDbContext).Name);
            }
        }

        private static IEnumerable<SubCategory> GetPreconfiguredSubCategories()
        {
            return new List<SubCategory>
            {
                new SubCategory { Code = "001", Description = "001 Description", CategoryId = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new SubCategory { Code = "002", Description = "002 Description", CategoryId = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product { SubcategoryId = 1, Ski = "001 Ski", Name = "Product 001", Description = "001 Product Description", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Product { SubcategoryId = 2, Ski = "002 Ski", Name = "Product 002", Description = "002 Product Description", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            };
        }
    }
}
