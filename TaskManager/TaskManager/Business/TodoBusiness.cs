using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Contract.Utilities;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class TodoBusiness : ITodoBusiness
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoEnricher _todoEnricher;
        private readonly IMapper _mapper;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly ICloneProvider _cloneProvider;

        public TodoBusiness(ITodoRepository repository
            , ITodoEnricher enricher
            , IMapper mapper
            , IIdentifierProvider identifierProvider
            , ICloneProvider cloneProvider
            )
        {
            _todoRepository = repository;
            _todoEnricher = enricher;
            _mapper = mapper;
            _identifierProvider = identifierProvider;
            _cloneProvider = cloneProvider;
        }

        public IEnumerable<MetaTodo> GetAllActives()
        {
            return _todoRepository
                .GetAllActives()
                .Select(t => _todoEnricher.Enrich(t))
                .OrderByDescending(t => t.MetaScore)
                .ToList();
        }

        public void IncrementScore(string todoId, int increment)
        {
            var todo = _todoRepository.Get(todoId);
            todo.Score += increment;
            SaveChanges(todo);
        }

        public void Complete(string todoId)
        {
            var todo = _todoRepository.Get(todoId);

            if (todo.Repeat != null)
            {
                var newTodo = _cloneProvider.DeepClone(todo);
                HandlerRepeat(newTodo);
            }

            todo.Completed = true;
            SaveChanges(todo);
        }

        private void HandlerRepeat(Todo newTodo)
        {
            newTodo.Completed = false;
            newTodo.DateCreated = DateTimeOffset.Now;
            newTodo.TodoId = _identifierProvider.CreateNew();

            var d = newTodo.Repeat.Type == RepeatType.After ? DateTimeOffset.Now
                : newTodo.Repeat.Type == RepeatType.Every ? newTodo.ReferenceDate.Value
                : throw new NotImplementedException();

            var c = newTodo.Repeat.Count;
            var u = newTodo.Repeat.Unit;
            var newReferenceDate = 
                u == RepeatUnit.Day ? d.AddDays(c)
                : u == RepeatUnit.Week ? d.AddDays(c * 7)
                : u == RepeatUnit.Month ? d.AddMonths(c)
                : u == RepeatUnit.Year ? d.AddYears(c)
                : throw new NotImplementedException();

            newTodo.ReferenceDate = newReferenceDate;

            _todoRepository.Upsert(newTodo);
        }

        public void Create(string title)
        {
            var todo = new Todo
            {
                TodoId = _identifierProvider.CreateNew(),
                Completed = false,
                Score = 0,
                Title = title,
                DateCreated = DateTimeOffset.Now,
                DateModified = DateTimeOffset.Now,
            };
            SaveChanges(todo);
        }

        public MetaTodo Get(string todoId)
        {
            var todo = _todoRepository.Get(todoId);
            var metaTodo = _todoEnricher.Enrich(todo);
            return metaTodo;
        }

        public void SaveChanges(Todo todo)
        {
            todo.DateModified = DateTimeOffset.Now;
            _todoRepository.Upsert(todo);
        }
    }
}
