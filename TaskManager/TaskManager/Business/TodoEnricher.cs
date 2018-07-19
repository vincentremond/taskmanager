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
            result.MetaScore = 1M * todo.Score - todo.Complexity / 60M;
            result.IsDraft = !HasMandatoryFields(todo);
            return result;
        }
        private bool HasMandatoryFields(Todo todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Title))
            {
                return false;
            }

            if (!(todo.Complexity > 0))
            {
                return false;
            }

            if (todo.Context == null)
            {
                return false;
            }

            if (todo.Project == null)
            {
                return false;
            }

            if (!todo.ReferenceDate.HasValue || todo.ReferenceDate.Value == default)
            {
                return false;
            }

            return true;
        }
    }
}
