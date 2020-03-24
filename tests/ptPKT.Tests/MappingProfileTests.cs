using System;
using AutoMapper;
using ptPKT.Core.Entities.BL;
using ptPKT.WebUI.Models;
using Xunit;

namespace ptPKT.Tests
{
    public class MappingProfileTests
    {
        [Fact]
        public void CreateMap_ToDoItemDtoToTodoItemResult_IsValid()
        {
            var config = new MapperConfiguration(cfg => 
                    cfg.CreateMap<ToDoItemDTO, ToDoItem>());
            config.AssertConfigurationIsValid();

            var ex = Assert.Throws<Exception>(() => config.AssertConfigurationIsValid());
            Assert.Null(ex);
        }
    }
}
