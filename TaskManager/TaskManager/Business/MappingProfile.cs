using AutoMapper;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, MetaTodo>()
                .ForMember(o => o.MetaScore, e => e.UseValue(0.0))
                .ForMember(o => o.IsDraft, e => e.UseValue(true))
                ;
        }
    }
}
