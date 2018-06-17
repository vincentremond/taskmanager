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
        private readonly ITodoRepository _repository;
        private readonly ITodoEnricher _enricher;

        public TodoBusiness(ITodoRepository repository, ITodoEnricher enricher)
        {
            _repository = repository;
            _enricher = enricher;
        }

        public IEnumerable<MetaTodo> GetAllActives()
        {
            return _repository
                .GetAllActives()
                .Select(t  => _enricher.Enrich(t))
                .OrderByDescending(t => t.MetaScore)
                .ToList();
        }

        public void IncrementScore(string todoId, int increment)
        {
            var todo = _repository.Get(todoId);
            todo.Score += increment;
            Save(todo);
        }

        public void Complete(string todoId)
        {
            var todo = _repository.Get(todoId);
            todo.Status = TodoStatus.Completed;
            Save(todo);
        }

        public void Create(string title)
        {
            var todo = new Todo
            {
                TodoId = Guid.NewGuid().ToString("N"),
                Status = TodoStatus.Active, // todo: set new tasks as draft
                Score = 0,
                Title = title,
                DateCreated = DateTimeOffset.Now,
                DateModified = DateTimeOffset.Now,
            };
            Save(todo);
        }

        private void Save(Todo todo)
        {
            todo.DateModified = DateTimeOffset.Now;
            _repository.Upsert(todo);
        }
    }
}
