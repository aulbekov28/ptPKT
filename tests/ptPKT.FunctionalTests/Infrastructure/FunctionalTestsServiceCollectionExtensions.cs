using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ptPKT.FunctionalTests.Infrastructure
{
    public static class FunctionalTestsServiceCollectionExtensions
    {
        public static IServiceCollection SetupTestDatabase<TContext>(this IServiceCollection services, string inMemoryDbName) where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<TContext>(option =>
            {
                option.UseInMemoryDatabase(inMemoryDbName);
            });

            return services;
        }
    }
}