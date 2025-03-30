using AutoFixture;
using NationsBenefits.Domain;
using NationsBenefits.Infrastructure.Persistence;

namespace NationsBenefits.Application.UnitTests.Mocks
{
    public static class MockSubCategoryRepository
    {
        public static void AddDataSubCategoryRepository(NationsBenefitsDbContext NationsBenefitsDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var subCategories = fixture.CreateMany<SubCategory>().ToList();            

            subCategories.Add(fixture.Build<SubCategory>()
                .With(tr => tr.Id, 8001)
                .Create()
            );

            NationsBenefitsDbContextFake.SubCategories!.AddRange(subCategories);
            NationsBenefitsDbContextFake.SaveChanges();
        }
    }
}
