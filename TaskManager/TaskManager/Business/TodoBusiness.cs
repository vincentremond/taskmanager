using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class TodoBusiness : ITodoBusiness
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoEnricher _todoEnricher;

        public TodoBusiness(ITodoRepository repository, ITodoEnricher enricher)
        {
            _todoRepository = repository;
            _todoEnricher = enricher;
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
            todo.Completed = true;
            SaveChanges(todo);
        }

        public void Create(string title)
        {
            var todo = new Todo
            {
                TodoId = Guid.NewGuid().ToString("N"),
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
