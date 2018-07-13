using AutoMapper;
using TaskManager.Contract.Business;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class TodoEnricher : ITodoEnricher
    {
        private readonly IMapper _mapper;

        public TodoEnricher(IMapper mapper)
        {
            _mapper = mapper;
        }

        public MetaTodo Enrich(Todo todo)
        {
            var result = _mapper.Map<MetaTodo>(todo);
            result.MetaScore = 100M * todo.Score / todo.Complexity;
            return result;
        }
    }
}
