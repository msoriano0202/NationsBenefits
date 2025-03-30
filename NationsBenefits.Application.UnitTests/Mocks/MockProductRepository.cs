using AutoFixture;
using EFCore.BulkExtensions;
using NationsBenefits.Domain;
using NationsBenefits.Infrastructure.Persistence;

namespace NationsBenefits.Application.UnitTests.Mocks
{
    public static class MockProductRepository
    {
        public static void AddDataProductRepository(NationsBenefitsDbContext NationsBenefitsDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var products = fixture.CreateMany<Product>().ToList();            

            products.Add(fixture.Build<Product>()
                .With(tr => tr.SubcategoryId, 8000)
                .With(tr => tr.Id, 8000)
                .Create()
            );

            NationsBenefitsDbContextFake.Products!.AddRange(products);
            NationsBenefitsDbContextFake.SaveChanges();
        }
    }
}
