using AutoMapper;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.ViewModel.Builder
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Edit, EditViewModel>()
                .ForMember(o => o.ViewData, e => e.Ignore())
                ;
        }
    }
}
