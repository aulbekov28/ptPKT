using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ptPKT.Infrastructure.Data;
using ptPKT.SharedKernel.Interfaces;
using Moq;
using ptPKT.Core.Interfaces;
using ptPKT.Core.Services.Identity;
using ptPKT.Tests;

namespace ptPKT.IntegrationTests
{
    public abstract class BaseEfRepoTestFixture
    {
        protected AppDbContext _dbContext;

        protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("testpetproject")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockDispatcher = new Mock<IDomainEventDispatcher>();
            var mockEnvironment = MockHelper.EnvironmentContextMock();

            _dbContext = new AppDbContext(options, mockDispatcher.Object, mockEnvironment.Object);
            return new EfRepository(_dbContext);
        }

    }
}
