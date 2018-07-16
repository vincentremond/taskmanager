using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.ViewModel.Builder
{
    public class TodoViewModelBuilder : ITodoViewModelBuilder
    {
        private readonly ITodoBusiness _todoBusiness;

        public TodoViewModelBuilder(ITodoBusiness todoBusiness)
        {
            _todoBusiness = todoBusiness;
        }

        public Index Index()
        {
            var todos = _todoBusiness.GetAllActives();
            var result = new Index
            {
                Items = todos.Select(t => new Index.Item()
                {
                    TodoId = t.TodoId,
                    Title = t.Title,
                    MetaScore = t.MetaScore,
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
            };
            return result;
        }

        public void Update(Edit model)
        {
            var todo = _todoBusiness.Get(model.TodoId);
            todo.Title = model.Title;
            todo.Complexity = model.Complexity;
            todo.Description = model.Description;
            todo.ContextId = model.ContextId;
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
