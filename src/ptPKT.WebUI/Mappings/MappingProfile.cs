using System;
using AutoMapper;
using ptPKT.Core.Entities.BL;
using ptPKT.WebUI.Models;

namespace ptPKT.WebUI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItemDTO, ToDoItem>();
            CreateMap<ToDoItem, ToDoItemDTO>();
        }
    }
}
