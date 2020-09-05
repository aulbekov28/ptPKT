using System;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ptPKT.Core.Entities;
using ptPKT.Core.Entities.BL;
using ptPKT.Infrastructure.Data;

namespace ptPKT.Tests
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var mockEnvironment = MockHelper.EnvironmentContextMock();

            using (var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), 
                                                    null,
                                                    mockEnvironment.Object))
            {
                // Look for any TODO items.
                if (dbContext.ToDoItems.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateTestData(dbContext);
            }
        }

        public static void PopulateTestData(AppDbContext dbContext)
        {
            var fixture = new Fixture();

            ToDoItem1 = fixture.Create<ToDoItem>();
            ToDoItem2 = fixture.Create<ToDoItem>();
            ToDoItem3 = fixture.Create<ToDoItem>();
            
            foreach (var item in dbContext.ToDoItems)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.ToDoItems.Add(ToDoItem1);
            dbContext.ToDoItems.Add(ToDoItem2);
            dbContext.ToDoItems.Add(ToDoItem3);

            dbContext.SaveChanges();
        }

        public static ToDoItem ToDoItem3 { get; set; }

        public static ToDoItem ToDoItem2 { get; set; }

        public static ToDoItem ToDoItem1 { get; set; }
    }
}
