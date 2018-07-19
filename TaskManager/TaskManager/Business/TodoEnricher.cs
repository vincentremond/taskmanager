using System;
using AutoMapper;
using AutoMapper.Mappers;
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
            result.IsDraft = !HasMandatoryFields(todo);
            result.MetaScore = GetMetaScore(result);
            return result;
        }

        private decimal GetMetaScore(MetaTodo todo)
        {
            if (todo.IsDraft)
            {
                return 0;
            }

            var days = (decimal)(todo.ReferenceDate.Value - DateTimeOffset.Now).TotalDays;

            return 1M * todo.Score
                   - todo.Complexity / 60M
                   - days;
                ;
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
