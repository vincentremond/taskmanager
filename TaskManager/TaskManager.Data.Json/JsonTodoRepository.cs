using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonTodoRepository : ITodoRepository
    {
        private readonly JsonFileRepository<Todo> _fileRepository;

        public JsonTodoRepository(IHostingEnvironment hostingEnvironment)
        {
            _fileRepository = new JsonFileRepository<Todo>(hostingEnvironment, "todo");
        }

        public Todo Get(string todoId)
        {
            using (var todos = _fileRepository.GetDataAsReadOnly())
            {
                return todos.Data.SingleOrDefault(t => t.TodoId == todoId);
            }
        }

        public IEnumerable<Todo> GetAllActives()
        {
            using (var todos = _fileRepository.GetDataAsReadOnly())
            {
                return todos.Data.Where(t => t.Status == TodoStatus.Active).ToList();
            }
        }

        public void Upsert(Todo todo)
        {
            using (var todos = _fileRepository.GetDataAsReadWrite())
            {
                todos.Set(t => t.TodoId == todo.TodoId, todo);
            }
        }
    }
}