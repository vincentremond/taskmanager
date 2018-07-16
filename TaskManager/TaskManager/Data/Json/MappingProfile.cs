using AutoMapper;
using TaskManager.Data.Json.DataObjects;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JsonTodo, Todo>()
                .ForMember(i => i.Context, opts => opts.Ignore())
                ;
            CreateMap<Todo, JsonTodo>()
                .ForMember(j => j.ContextId, opts => opts.MapFrom(s => s.Context.ContextId))
                ;
        }
    }
}
