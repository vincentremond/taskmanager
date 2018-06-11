using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonTodoRepository : ITodoRepository
    {
        private static readonly ConcurrentDictionary<string, Todo> data;

        static JsonTodoRepository()
        {

            data = new ConcurrentDictionary<string, Todo>();

            void AddItem(int score, TodoStatus status, string title)
            {
                var todoId = Guid.NewGuid().ToString("N");
                data[todoId] = new Todo
                {
                    TodoId = todoId,
                    DateCreated = DateTimeOffset.Now,
                    DateModified = DateTimeOffset.Now,
                    Title = title,
                    Status = status,
                    Score = score,
                };
            };

            var index = 100;
            AddItem(--index, TodoStatus.Active, "Really persist task to disk");
            AddItem(--index, TodoStatus.Active, "Todo is a draft before being active");
            AddItem(--index, TodoStatus.Active, "Todo has a duration");
            AddItem(--index, TodoStatus.Active, "Todo has an url");
            AddItem(--index, TodoStatus.Active, "Todo has a project");
            AddItem(--index, TodoStatus.Active, "Todo has a context");
            AddItem(--index, TodoStatus.Active, "Todo has a description");
            AddItem(--index, TodoStatus.Active, "Todos not modified since more than 7 days should be reviewed");
            AddItem(--index, TodoStatus.Active, "Todo can be deleted");
        }

        public IEnumerable<Todo> GetAllActives()
        {
            var todos = data.Values.Where(t => t.Status == TodoStatus.Active).ToList();
            todos.ForEach(ComputeMetaScore);
            return todos.OrderByDescending(t => t.MetaScore).ToList();
        }

        public void ComputeMetaScore(Todo todo)
        {
            todo.MetaScore = todo.Score;
        }

        public Todo Get(string todoId)
        {
            return data.TryGetValue(todoId, out var todo) ? todo : null;
        }

        public void Upsert(Todo todo)
        {
            data[todo.TodoId] = todo;
        }
    }
}