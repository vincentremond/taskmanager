using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using TaskManager.Contract.Data;
using TaskManager.Data.Json.DataObjects;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonTodoRepository : ITodoRepository
    {
        private readonly IMapper _mapper;
        private readonly IContextRepository _contextRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly JsonFileRepository<JsonTodo> _fileRepository;

        public JsonTodoRepository(IHostingEnvironment hostingEnvironment
            , IMapper mapper
            , IContextRepository contextRepository
            , IProjectRepository projectRepository
            )
        {
            _mapper = mapper;
            _contextRepository = contextRepository;
            _projectRepository = projectRepository;
            _fileRepository = new JsonFileRepository<JsonTodo>(hostingEnvironment, "todo");
        }

        public Todo Get(string todoId)
        {
            using (var todos = _fileRepository.GetDataAsReadOnly())
            {
                var contexts = _contextRepository.GetAll();
                var projects = _projectRepository.GetAll();
                return Convert(todos.Data.SingleOrDefault(t => t.TodoId == todoId), contexts, projects);
            }
        }

        public IEnumerable<Todo> GetAllActives()
        {
            using (var todos = _fileRepository.GetDataAsReadOnly())
            {
                var contexts = _contextRepository.GetAll();
                var projects = _projectRepository.GetAll();
                return todos
                    .Data
                    .Where(t => !t.Completed)
                    .Select(j => Convert(j, contexts, projects))
                    .ToList();
            }
        }

        public void Upsert(Todo todo)
        {
            using (var todos = _fileRepository.GetDataAsReadWrite())
            {
                todos.Set(t => t.TodoId == todo.TodoId, Convert(todo));
            }
        }

        private Todo Convert(JsonTodo input, IEnumerable<Context> contexts, IEnumerable<Project> projects)
        {
            if (input == null)
            {
                return null;
            }
            
            var result = _mapper.Map<Todo>(input);
            result.Context = contexts.SingleOrDefault(c => c.ContextId == input.ContextId);
            result.Project = projects.SingleOrDefault(c => c.ProjectId == input.ProjectId);
            return result;
        }

        private JsonTodo Convert(Todo input)
        {
            if (input == null)
            {
                return null;
            }

            var result = _mapper.Map<JsonTodo>(input);
            result.ContextId = input.Context?.ContextId;
            result.ProjectId = input.Project?.ProjectId;
            return result;
        }
    }
}