using Microsoft.EntityFrameworkCore;
using ptPKT.Core.Entities;
using System.Linq;
using ptPKT.Tests;
using Xunit;

namespace ptPKT.IntegrationTests.Data
{
    public class EfRepositoryAdd : BaseEfRepoTestFixture
    {
        [Fact]
        public void AddsItemAndSetsId()
        {
            var repository = GetRepository();
            var item = new ToDoItemBuilder().Build();

            repository.Add(item);

            var newItem = repository.List<ToDoItem>().FirstOrDefault();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id > 0);
        }
    }
}
