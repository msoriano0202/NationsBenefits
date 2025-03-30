using Microsoft.EntityFrameworkCore;
using NationsBenefits.Domain;
using NationsBenefits.Domain.Common;

namespace NationsBenefits.Infrastructure.Persistence
{
    public class NationsBenefitsDbContext : DbContext
    {
        public NationsBenefitsDbContext(DbContextOptions<NationsBenefitsDbContext> options): base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategory>().Metadata.SetSchema("product");
            modelBuilder.Entity<Product>().Metadata.SetSchema("product");

            //modelBuilder.Entity<SubCategory>()
            //   .HasMany(m => m.Products)
            //   .WithOne(m => m.SubCategory)
            //   .HasForeignKey(m => m.Subcategory_id)
            //   .IsRequired()
            //   .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Product>? Products { get; set; }

        public DbSet<SubCategory>? SubCategories { get; set; }
    }
}
