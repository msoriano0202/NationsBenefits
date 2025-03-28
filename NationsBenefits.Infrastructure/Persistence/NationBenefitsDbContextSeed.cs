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
                new SubCategory { Code = "001", Description = "001 Description", Category_Id = 1, created_at = DateTime.Now, updated_at = DateTime.Now },
                new SubCategory { Code = "002", Description = "002 Description", Category_Id = 2, created_at = DateTime.Now, updated_at = DateTime.Now },
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product { Subcategory_id = 1, Ski = "001 Ski", Name = "Product 001", Description = "001 Product Description", created_at = DateTime.Now, updated_at = DateTime.Now },
                new Product { Subcategory_id = 2, Ski = "002 Ski", Name = "Product 002", Description = "002 Product Description", created_at = DateTime.Now, updated_at = DateTime.Now },
            };
        }
    }
}
