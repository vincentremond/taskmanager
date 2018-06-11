using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model;

namespace TaskManager.ViewModel.Builder
{
    public class TodoViewModelBuilder : ITodoViewModelBuilder
    {
        private readonly ITodoBusiness _todoBusiness;

        public TodoViewModelBuilder(ITodoBusiness todoBusiness)
        {
            _todoBusiness = todoBusiness;
        }

        public TodoIndexViewModel GetIndex()
        {
            var todos = _todoBusiness.GetAllActives();
            var result = new TodoIndexViewModel();
            result.Items = todos.Select(t => new TodoIndexViewModel.Item()
            {
                TodoId = t.TodoId,
                Title = t.Title,
                MetaScore =  t.MetaScore,
                Score = t.Score,
            }).ToList();
            return result;
        }
    }
}
