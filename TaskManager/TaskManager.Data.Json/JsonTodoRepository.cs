using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonTodoRepository : ITodoRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public JsonTodoRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public Todo Get(string todoId)
        {
            var data = ReadModel();
            return data.SingleOrDefault(t => t.TodoId == todoId);
        }

        public IEnumerable<Todo> GetAllActives()
        {
            var data = ReadModel();
            return data.Where(t => t.Status == TodoStatus.Active);
        }

        public void Upsert(Todo todo)
        {
            var data = ReadModel();
            var index = data.FindIndex(t => t.TodoId == todo.TodoId);
            if (index == -1)
            {
                data.Add(todo);
            } else
            {
                data[index] = todo;
            }
            WriteModel(data);
        }

        #region File actions

        private List<Todo> ReadModel()
        {
            var file = GetFilePath();
            if (!File.Exists(file))
            {
                return new List<Todo>();
            }
            var contents = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<List<Todo>>(contents);
        }

        private void WriteModel(List<Todo> data)
        {
            var file = GetFilePath();
            var contents = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(file, contents);
        }

        private string GetFilePath()
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "data.json");
        }
        #endregion
    }
}