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

            void AddItem(string todoId, string title, bool completed, int score)
            {
                data[todoId] = new Todo
                {
                    TodoId = todoId,
                    Title = title,
                    Completed = completed,
                    Score = score,
                };
            };

            AddItem("dda44d8ecbb74b07bcd69ad1599e6a8f", "Task 1", false, 10);
            AddItem("79075496f92148568bb27669cbc7d7c4", "Task 2", false, 12);
            AddItem("243af39779d54d70b8642bdef9b4b7eb", "Task 0", true, 13);
        }

        public IEnumerable<Todo> GetAllActives()
        {
            var todos = data.Values.Where(t => !t.Completed).ToList();
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