using System;
using System.Collections.Generic;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class TodoBusiness : ITodoBusiness
    {
        private readonly ITodoRepository _repository;

        public TodoBusiness(ITodoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Todo> GetAllActives()
        {
            return _repository.GetAllActives();
        }

        public void IncrementScore(string todoId, int increment)
        {
            var todo = _repository.Get(todoId);
            todo.Score += increment;
            _repository.Upsert(todo);
        }

        public void Complete(string todoId)
        {
            var todo = _repository.Get(todoId);
            todo.Status = TodoStatus.Completed;
            _repository.Upsert(todo);
        }

        public void Create(string title)
        {
            var newTodo = new Todo
            {
                TodoId = Guid.NewGuid().ToString("N"),
                Status = TodoStatus.Active, // todo: set new tasks as draft
                Score = 0,
                Title = title,
            };
            _repository.Upsert(newTodo);
        }
    }
}
