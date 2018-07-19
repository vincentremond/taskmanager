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

        public TodoBusiness(ITodoRepository repository
            , ITodoEnricher enricher
            , IMapper mapper
            , IIdentifierProvider identifierProvider
            )
        {
            _todoRepository = repository;
            _todoEnricher = enricher;
            _mapper = mapper;
            _identifierProvider = identifierProvider;
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
                CreateRepeat(todo);
            }

            todo.Completed = true;
            SaveChanges(todo);
        }

        private void CreateRepeat(Todo todo)
        {
            var newTodo = _mapper.Map<Todo>(todo);

            newTodo.TodoId = _identifierProvider.CreateNew();

            // TODO update reference date

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
