using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.ViewModel.Builder
{
    public class TodoViewModelBuilder : ITodoViewModelBuilder
    {
        private readonly ITodoBusiness _todoBusiness;
        private readonly IContextBusiness _contextBusiness;

        public TodoViewModelBuilder(ITodoBusiness todoBusiness, IContextBusiness contextBusiness)
        {
            _todoBusiness = todoBusiness;
            _contextBusiness = contextBusiness;
        }

        public Index Index()
        {
            var todos = _todoBusiness.GetAllActives();
            var result = new Index
            {
                Drafts = new Index.DraftInfos
                {
                    Count = todos.Count(t => t.IsDraft),
                    FirstTodoId = todos.FirstOrDefault(t => t.IsDraft)?.TodoId,
                },
                Items = todos
                .Where(t => !t.IsDraft)
                    .Select(t => new Index.Item()
                    {
                        TodoId = t.TodoId,
                        Title = t.Title,
                        MetaScore = t.MetaScore,
                        Context = t.Context.Title,
                    }).ToList()
            };
            return result;
        }

        public Edit Edit(string id)
        {
            var todo = _todoBusiness.Get(id);
            var result = new Edit
            {
                TodoId = todo.TodoId,
                Title = todo.Title,
                Complexity = todo.Complexity,
                Description = todo.Description,
                ContextId = todo.Context?.ContextId,
            };
            return result;
        }

        public void Update(Edit model)
        {
            var todo = _todoBusiness.Get(model.TodoId);
            var context = _contextBusiness.Get(model.ContextId);
            todo.Title = model.Title;
            todo.Complexity = model.Complexity;
            todo.Description = model.Description;
            todo.Context = context;
            _todoBusiness.SaveChanges(todo);
        }

        public void Complete(string todoId)
        {
            _todoBusiness.Complete(todoId);
        }

        public void IncrementScore(string todoId, int increment)
        {
            _todoBusiness.IncrementScore(todoId, increment);
        }

        public void Create(string title)
        {
            _todoBusiness.Create(title);
        }
    }
}
