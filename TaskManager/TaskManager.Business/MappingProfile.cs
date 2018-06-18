using AutoMapper;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, MetaTodo>();
        }
    }
}
