using AutoMapper;
using TaskManager.Data.Json.DataObjects;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JsonTodo, Todo>();
            CreateMap<Todo, JsonTodo>();
        }
    }
}
