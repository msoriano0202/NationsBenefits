using Microsoft.EntityFrameworkCore;
using Moq;
using NationsBenefits.Infrastructure.Persistence;
using NationsBenefits.Infrastructure.Repositories;

namespace NationsBenefits.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<NationsBenefitsDbContext>()
                .UseInMemoryDatabase(databaseName: $"NationsBenefitsDbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new NationsBenefitsDbContext(options);
            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);

            return mockUnitOfWork;
        }

    }
}
